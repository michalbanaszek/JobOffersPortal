using JobOffersPortal.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobOffersPortal.Domain.Entities
{
    public class RefreshToken : AuditableEntity
    {
        [Key]
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
    }
}
