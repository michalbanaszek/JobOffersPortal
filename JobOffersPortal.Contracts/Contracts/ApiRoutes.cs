namespace JobOffersPortal.WebUI
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class Company
        {
            public const string GetAll = Base + "/company";

            public const string Update = Base + "/company/{id}";

            public const string Delete = Base + "/company/{id}";

            public const string Get = Base + "/company/{id}";

            public const string Create = Base + "/company";
        }

        public static class JobOffer
        {
            public const string GetAll = Base + "/joboffer";

            public const string Update = Base + "/joboffer/{id}";

            public const string Delete = Base + "/joboffer/{id}";

            public const string Get = Base + "/joboffer/{id}";

            public const string Create = Base + "/joboffer";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string RefreshToken = Base + "/identity/RefreshToken";

            public const string FacebookAuth = Base + "/identity/auth/fb";
        }
    }
}
