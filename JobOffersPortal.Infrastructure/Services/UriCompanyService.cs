using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Interfaces;
using System;

namespace JobOffersPortal.Persistance.EF.Services
{
    public class UriCompanyService : UriService, IUriCompanyService
    {
        public UriCompanyService(string baseUri) : base(baseUri)
        {  
        }

        public Uri GetCompanyUri(string companyId)
        {
            return new Uri(_baseUri + ApiRoutes.CompanyRoute.Get.Replace("{id}", companyId));
        }
    }
}
