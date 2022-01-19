namespace JobOffersPortal.Application
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class CompanyRoute
        {
            public const string GetAll = Base + "/companies";

            public const string GetAllCompanies = Base + "/onlycompanies";

            public const string Update = Base + "/company/{id}";

            public const string Delete = Base + "/company/{id}";

            public const string Get = Base + "/company/{id}";

            public const string Create = Base + "/company";
        }

        public static class JobOfferRoute
        {
            public const string GetAll = Base + "/joboffer";

            public const string Update = Base + "/joboffer/{id}";

            public const string Delete = Base + "/joboffer/{id}";

            public const string Get = Base + "/joboffer/{id}";

            public const string Create = Base + "/joboffer";
        }

        public static class JobOfferPropositionRoute
        {
            public const string GetAll = Base + "/jobofferPropositions/{jobOfferId}";

            public const string Update = Base + "/jobofferProposition/{id}";

            public const string Delete = Base + "/jobofferProposition/{id}";

            public const string Get = Base + "/jobofferProposition/{id}";

            public const string Create = Base + "/jobofferProposition";
        }

        public static class JobOfferRequirementRoute
        {
            public const string GetAll = Base + "/jobofferRequirements/{jobOfferId}";

            public const string Update = Base + "/jobofferRequirement/{id}";

            public const string Delete = Base + "/jobofferRequirement/{id}";

            public const string Get = Base + "/jobofferRequirement/{id}";

            public const string Create = Base + "/jobofferRequirement";
        }

        public static class JobOfferSkillRoute
        {
            public const string GetAll = Base + "/jobofferSkills/{jobOfferId}";

            public const string Update = Base + "/jobofferSkill/{id}";

            public const string Delete = Base + "/jobofferSkill/{id}";

            public const string Get = Base + "/jobofferSkill/{id}";

            public const string Create = Base + "/jobofferSkill";
        }

        public static class IdentityRoute
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string RefreshToken = Base + "/identity/RefreshToken";

            public const string FacebookAuth = Base + "/identity/auth/fb";
        }

        public static class UserRoute
        {
            public const string Get = Base + "/identity/users/{userId}";

            public const string Create = Base + "/identity/users/";

            public const string Delete = Base + "/identity/users/{id}";          
        }
    }
}
