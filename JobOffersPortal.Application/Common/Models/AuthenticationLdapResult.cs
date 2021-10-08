using Application.Common.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Common.Models
{
    public class AuthenticationLdapResult
    {
        public IAppUser User { get; set; }
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public bool Success { get; set; }       
        public IEnumerable<string> Errors { get; set; }
    }
}
