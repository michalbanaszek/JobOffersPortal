﻿using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples
{
    public class CreateCompanyCommandExample : IExamplesProvider<CreateCompanyCommand>
    {
        public CreateCompanyCommand GetExamples()
        {
            return new CreateCompanyCommand()
            {
                Name = "NewCompany"
            };
        }
    }
}
