using System;
using AduabaNeptune.Helper;

namespace AduabaNeptune.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(Filter filter, string route);
    }
}