using Microsoft.AspNetCore.Authentication.Cookies;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Options;

namespace WebApp.ClientServices.Security
{
    public class AuthenticationLdapService : IAuthenticationLdapMvcService
    {
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";

        private readonly LdapOptions _ldapOptions;
        private readonly LdapConnection _connection;

        private readonly IIdentityMvcService _identityMvcService;

        public AuthenticationLdapService(LdapOptions ldapOptions, IIdentityMvcService identityMvcService)
        {
            _ldapOptions = ldapOptions;
            _connection = new LdapConnection();
            _identityMvcService = identityMvcService;
        }

        public async Task<AuthenticationLdapResult> LoginAsync(string username, string password)
        {
            _connection.Connect(_ldapOptions.Url, LdapConnection.DEFAULT_PORT);
            _connection.Bind(_ldapOptions.Username, _ldapOptions.Password);

            var searchFilter = string.Format(_ldapOptions.SearchFilter, username);
            var result = _connection.Search(
                _ldapOptions.SearchBase,
                LdapConnection.SCOPE_SUB,
                searchFilter,
                new[] {
                    MemberOfAttribute,
                    DisplayNameAttribute,
                    SAMAccountNameAttribute,
                    MailAttribute
                },
                false
            );

            try
            {
                var user = result.Next();
                AuthenticationLdapResult authResponse = null;

                if (user != null)
                {
                    _connection.Bind(user.DN, password);

                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.getAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr == null)
                        {
                            authResponse = new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing the account name." },
                                Success = false
                            };

                            return authResponse;
                        }

                        var displayNameAttr = user.getAttribute(DisplayNameAttribute);
                        if (displayNameAttr == null)
                        {
                            authResponse = new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing the display name." },
                                Success = false
                            };

                            return authResponse;
                        }

                        var emailAttr = user.getAttribute(MailAttribute);
                        if (emailAttr == null)
                        {
                            authResponse = new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing an email." },
                                Success = false
                            };

                            return authResponse;
                        }

                        var memberAttr = user.getAttribute(MemberOfAttribute);
                        if (memberAttr == null)
                        {
                            authResponse = new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing roles." },
                                Success = false
                            };

                            return authResponse;
                        }

                        authResponse = new AuthenticationLdapResult()
                        {
                            User = new DomainUser
                            {
                                DisplayName = displayNameAttr.StringValue,
                                Username = accountNameAttr.StringValue,
                                Email = emailAttr.StringValue,
                                Roles = memberAttr.StringValueArray
                                .Select(x => GetGroup(x))
                                .Where(x => x != null)
                                .Distinct()
                                .ToArray()
                            },
                            Success = true
                        };
                    }
                }

                // If the user is authenticated, store its claims to cookie
                if (authResponse != null && authResponse.Success)
                {
                    var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, authResponse.User.Username),
                            new Claim(ClaimTypes.Email, authResponse.User.Email)                           
                        };

                    // Roles
                    foreach (var role in authResponse.User.Roles)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    //we can add custom claims based on the AD user's groups
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    if (Array.Exists(authResponse.User.Roles, s => s.Contains("Jobs_App")))
                    {
                        //if in the AD the user belongs to the aspnetcore.ldap group, we add a claim
                        claimsIdentity.AddClaim(new Claim("Read", "true"));

                        var registerResult = await _identityMvcService.RegisterAsync(authResponse.User.Email, password);

                        if (!registerResult.Success)
                        {
                            await _identityMvcService.LoginAsync(authResponse.User.Email, password);
                        }
                    }

                    authResponse.ClaimsIdentity = claimsIdentity;

                    return authResponse;
                }

                return authResponse;
            }
            finally
            {
                _connection.Disconnect();
            }
        }

        private static string GetGroup(string value)
        {
            Match match = Regex.Match(value, "^CN=([^,]*)");
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }
    }
}