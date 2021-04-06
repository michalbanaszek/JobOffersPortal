using Application;
using Application.Common.Interfaces;
using System;

namespace Infrastructure.Services
{
    public class UriJobOfferService : UriService, IUriJobOfferService
    {
        private readonly string _baseUri;

        public UriJobOfferService(string baseUri) : base(baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetJobOfferUri(string jobOfferId)
        {
            return new Uri(_baseUri + ApiRoutes.JobOfferRoute.Get.Replace("{id}", jobOfferId));
        }
    }
}
