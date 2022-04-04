using System;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUriService
    {
        Uri GetAll(int pageNumber, int pageSize);
        Uri Get(string id, string controller);
    }
}
