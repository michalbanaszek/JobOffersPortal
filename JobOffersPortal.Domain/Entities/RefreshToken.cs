using JobOffersPortal.Domain.Primitives;
using System;

namespace JobOffersPortal.Domain.Entities
{
    public sealed class RefreshToken : Entity
    {
        public RefreshToken(string token, string jwtId, DateTime creationDate, DateTime expiryDate, bool used) : base(token)
        {
            Token = token;
            JwtId = jwtId;
            CreationDate = creationDate;
            ExpiryDate = expiryDate;
            Used = used;
        }

        public string Token { get; private set; }

        public string JwtId { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime ExpiryDate { get; private set; }

        public bool Used { get; private set; }
    }
}
