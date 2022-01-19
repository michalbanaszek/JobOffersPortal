using JobOffersPortal.Domain.Common;
using System;

namespace JobOffersPortal.Domain.Entities
{
    public class RefreshToken : AuditableEntity
    {      
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
    }
}
