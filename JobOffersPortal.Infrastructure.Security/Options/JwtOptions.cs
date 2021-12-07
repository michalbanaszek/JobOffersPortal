using System;

namespace JobOffersPortal.Infrastructure.Security.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}
