using Application.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllUri(int pageNumber, int pageSize)
        {
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
