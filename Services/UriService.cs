using System;
using AduabaNeptune.Helper;
using Microsoft.AspNetCore.WebUtilities;

namespace AduabaNeptune.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        
        public Uri GetPageUri(Filter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));

            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            
            return new Uri(modifiedUri);
        }
    }
}