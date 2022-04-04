using JobOffersPortal.Application.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace JobOffersPortal.Persistance.EF.Services
{
    public class UriService : IUriService
    {
        protected readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAll(int pageNumber, int pageSize)
        {
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());

            return new Uri(modifiedUri);
        }

        public Uri Get(string id, string controller)
        {
            string location = String.Concat(_baseUri, "api", "/", $"{controller}", "/", $"{id}");

            return new Uri(location);
        }
    }
}
