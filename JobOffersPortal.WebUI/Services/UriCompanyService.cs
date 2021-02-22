using System;

namespace JobOffersPortal.WebUI.Services
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
            return new Uri(_baseUri + ApiRoutes.Company.Get.Replace("{id}", companyId));
        }
    }
}
