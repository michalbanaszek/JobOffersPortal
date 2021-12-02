namespace JobOffersPortal.Application
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class CompanyRoute
        {
            public const string GetAllCompanies = Base + "/companies";

            public const string GetJustCompanies = Base + "/onlycompanies";

            public const string Update = Base + "/company/{id}";

            public const string Delete = Base + "/company/{id}";

            public const string Get = Base + "/company/{id}";

            public const string Create = Base + "/company";
        }

        public static class JobOfferRoute
        {
            public const string GetJobOffers = Base + "/joboffer";

            public const string Update = Base + "/joboffer/{id}";

            public const string Delete = Base + "/joboffer/{id}";

            public const string Get = Base + "/joboffer/{id}";

            public const string Create = Base + "/joboffer";
        }

        public static class IdentityRoute
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string RefreshToken = Base + "/identity/RefreshToken";

            public const string FacebookAuth = Base + "/identity/auth/fb";
        }
    }
}
