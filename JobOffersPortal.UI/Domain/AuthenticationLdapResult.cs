﻿using System.Collections.Generic;
using System.Security.Claims;
using WebApp.Interfaces;

namespace WebApp.Domain
{
    public class AuthenticationLdapResult
    {
        public IMvcDomainUser User { get; set; }
        public string Token { get; set; }
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
