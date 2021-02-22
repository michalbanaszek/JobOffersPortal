using JobOffersPortal.WebUI.Contracts.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace JobOffersPortal.WebUI.SwaggerExamples.Responses
{
    public class CompanyResponseExamples : IExamplesProvider<CompanyResponse>
    {
        public CompanyResponse GetExamples()
        {
            return new CompanyResponse()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "NewCompany",
                UserId = Guid.NewGuid().ToString(),
                JobOffers = new List<JobOfferResponse>()
                {
                    new JobOfferResponse() { Id = Guid.NewGuid().ToString() }
                }
            };
        }
    }
}
