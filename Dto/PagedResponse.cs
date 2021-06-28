using System;

namespace AduabaNeptune.Dto
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            Succeeded = true;
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Message = string.Empty;
            Errors = null;
        }
    }
}