using System;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUriJobOfferService : IUriService
    {
        Uri GetJobOfferUri(string jobOfferId);
    }
}
