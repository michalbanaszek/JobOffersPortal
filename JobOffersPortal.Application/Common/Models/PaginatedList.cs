using JobOffersPortal.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize, string nextPage, string previousPage)
        {
            PageNumber = pageNumber;
            PageSize = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            NextPage = nextPage;
            PreviousPage = previousPage;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < PageSize;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, IUriService uriService)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var nextPage = pageNumber >= 1
                ? uriService.GetAllUri(pageNumber + 1, pageSize).ToString()
                : null;

            var previousPage = pageNumber - 1 >= 1
                ? uriService.GetAllUri(pageNumber - 1, pageSize).ToString()
                : null;

            return new PaginatedList<T>(items, count, pageNumber, pageSize, nextPage, previousPage);
        }
    }
}
