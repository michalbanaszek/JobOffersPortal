using System;

namespace JobOffersPortal.WebUI.Services
{
    public interface IUriJobOfferService : IUriService
    {
        Uri GetJobOfferUri(string jobOfferId);
    }
}
