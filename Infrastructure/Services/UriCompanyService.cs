using Application;
using Application.Common.Interfaces;
using System;

namespace Infrastructure.Services
{
    public class UriCompanyService : UriService, IUriCompanyService
    {
        private readonly string _baseUri;

        public UriCompanyService(string baseUri) : base(baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetCompanyUri(string companyId)
        {
            return new Uri(_baseUri + ApiRoutes.CompanyRoute.Get.Replace("{id}", companyId));
        }
    }
}
