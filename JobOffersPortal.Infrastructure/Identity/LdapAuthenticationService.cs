using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Models.Responses;
using JobOffersPortal.Persistance.EF.Options;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System.Linq;
using System.Text.RegularExpressions;

namespace JobOffersPortal.Persistance.EF.Identity
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";

        private readonly LdapOptions _config;
        private readonly LdapConnection _connection;

        public LdapAuthenticationService(IOptions<LdapOptions> configAccessor)
        {
            _config = configAccessor.Value;
            _connection = new LdapConnection();
        }

        public AuthenticationLdapResult Login(string username, string password)
        {
            _connection.Connect(_config.Url, LdapConnection.DEFAULT_PORT);
            _connection.Bind(_config.Username, _config.Password);

            var searchFilter = string.Format(_config.SearchFilter, username);
            var result = _connection.Search(
                _config.SearchBase,
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

                if (user != null)
                {
                    _connection.Bind(user.DN, password);
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.getAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr == null)
                        {
                            return new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing the account name." },
                                Success = false
                            };
                        }

                        var displayNameAttr = user.getAttribute(DisplayNameAttribute);
                        if (displayNameAttr == null)
                        {
                            return new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing the display name." },
                                Success = false
                            };
                        }

                        var emailAttr = user.getAttribute(MailAttribute);
                        if (emailAttr == null)
                        {
                            return new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing an email." },
                                Success = false
                            };
                        }

                        var memberAttr = user.getAttribute(MemberOfAttribute);
                        if (memberAttr == null)
                        {
                            return new AuthenticationLdapResult()
                            {
                                Errors = new[] { "Your account is missing roles." },
                                Success = false
                            };
                        }

                        return new AuthenticationLdapResult()
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
            }
            finally
            {
                _connection.Disconnect();
            }

            return null;
        }

        private string GetGroup(string value)
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

