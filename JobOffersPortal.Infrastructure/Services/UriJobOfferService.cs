using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Interfaces;
using System;

namespace JobOffersPortal.Persistance.EF.Services
{
    public class UriJobOfferService : UriService, IUriJobOfferService
    {
        public UriJobOfferService(string baseUri) : base(baseUri)
        {
        }

        public Uri GetJobOfferUri(string jobOfferId)
        {
            return new Uri(_baseUri + ApiRoutes.JobOfferRoute.Get.Replace("{id}", jobOfferId));
        }
    }
}
