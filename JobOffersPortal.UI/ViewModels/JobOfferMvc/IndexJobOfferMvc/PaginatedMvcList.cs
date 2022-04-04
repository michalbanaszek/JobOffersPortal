using System.Collections.Generic;

namespace JobOffersPortal.UI.ViewModels.JobOfferMvc.IndexJobOfferMvc
{
    public class PaginatedMvcList<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
