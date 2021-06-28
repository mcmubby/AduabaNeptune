using System;
using AduabaNeptune.Helper.Pagination;

namespace AduabaNeptune.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(Filter filter, string route);
    }
}