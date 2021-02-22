using JobOffersPortal.Contracts.Contracts.Queries;
using JobOffersPortal.Contracts.Contracts.Responses;
using JobOffersPortal.WebUI.Domain;
using JobOffersPortal.WebUI.Services;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.WebUI.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter paginationFilter, List<T> response)
        {
            var nextPage = paginationFilter.PageNumber >= 1
              ? uriService.GetAllUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
              : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService.GetAllUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                : null;

            return new PagedResponse<T>()
            {
                Data = response,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
