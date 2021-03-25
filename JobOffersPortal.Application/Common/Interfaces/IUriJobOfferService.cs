using System;

namespace Application.Common.Interfaces
{
    public interface IUriJobOfferService : IUriService
    {
        Uri GetJobOfferUri(string jobOfferId);
    }
}
