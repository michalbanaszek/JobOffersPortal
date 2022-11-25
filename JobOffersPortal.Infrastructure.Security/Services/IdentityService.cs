using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Security.Services;
using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Domain.Models;
using JobOffersPortal.Infrastructure.Security.Options;
using JobOffersPortal.Infrastructure.Security.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobOffersPortal.Infrastructure.Security.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IApplicationDbContext _context;
        private readonly IFacebookAuthService _facebookAuthService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            TokenValidationParameters tokenValidationParameters,
            IApplicationDbContext context,
            IFacebookAuthService facebookAuthService,
            JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _facebookAuthService = facebookAuthService;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "email or password is wrong." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "email or password is wrong." }
                };
            }

            return await GenerateTokenForUserAsync(user.Email);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "Invalid token" }
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                        .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This token hasn't expired yet" }
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token does not exist" }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This token has expired" }
                };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This token has been used" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token does not match this JWT" }
                };
            }

           // storedRefreshToken.Used = true;

            _context.RefreshTokens.Update(storedRefreshToken);

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await GenerateTokenForUserAsync(user.Email);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "User with this email address already exists." }
                };
            }

            var newUser = new ApplicationUser()
            {
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                return new AuthenticationResult()
                {
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

            await _userManager.AddClaimAsync(newUser, new Claim("Create Role", "true"));

            return await GenerateTokenForUserAsync(newUser.Email);
        }

        public async Task<AuthenticationResult> LoginFacebookAsync(string accessToken)
        {
            var validatedTokenResponse = await _facebookAuthService.ValidateAccessTokenAsync(accessToken);

            if (!validatedTokenResponse.Success)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "Invalid Facebook token" }
                };
            }

            var userInfoResponse = await _facebookAuthService.GetUserInfoAsync(accessToken);

            if (!userInfoResponse.Success)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "Invalid user" }
                };
            }

            var user = await _userManager.FindByEmailAsync(userInfoResponse.Data.Email);

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfoResponse.Data.Email,
                    UserName = userInfoResponse.Data.Email
                };

                var createdResult = await _userManager.CreateAsync(user);

                if (!createdResult.Succeeded)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new[] { "Something went wrong" }
                    };
                }

                return await GenerateTokenForUserAsync(user.Email);
            }

            return await GenerateTokenForUserAsync(user.Email);
        }

        public async Task<AuthenticationResult> LoginLdap(string email, string password)
        {
            var authResult = await GenerateTokenForUserAsync(email);

            return authResult;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validationToken);

                if (!IsJwtWithValidSecurityAlgorithm(validationToken))
                {
                    return null;
                }
                else
                {
                    return principal;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateTokenForUserAsync(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                userClaims.Add(new Claim("id", user.Id));
                claims.AddRange(userClaims);
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_jwtOptions.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    Success = true,
                    Token = tokenHandler.WriteToken(token)
                };
            }

            var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), token.Id, DateTime.UtcNow, DateTime.UtcNow.AddMonths(6), false) 
            {
                CreatedBy = user.Id,
            };

            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();

            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}