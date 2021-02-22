using System;
using System.ComponentModel.DataAnnotations;

namespace JobOffersPortal.WebUI.Domain
{
    public class RefreshToken : BaseModel
    {
        [Key]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
    }
}
