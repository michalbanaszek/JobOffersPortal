using System;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUriService
    {
        Uri GetAllUri(int pageNumber, int pageSize);
    }
}
