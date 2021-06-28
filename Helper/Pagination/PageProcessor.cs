using System;
using System.Collections.Generic;
using AduabaNeptune.Dto;
using AduabaNeptune.Services;

namespace AduabaNeptune.Helper.Pagination
{
    public static class PageProcessor
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(this List<T> pagedData, Filter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);

            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            //Generate link to next page
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;

            //Previous page link implementation    
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

            //First and last page link
            response.FirstPage = uriService.GetPageUri(new Filter(1, validFilter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new Filter(roundedTotalPages, validFilter.PageSize), route);
            
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}