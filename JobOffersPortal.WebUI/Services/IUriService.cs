using JobOffersPortal.Contracts.Contracts.Queries;
using System;

namespace JobOffersPortal.WebUI.Services
{
    public interface IUriService
    {
        Uri GetAllUri(PaginationQuery pagination = null);
    }
}
