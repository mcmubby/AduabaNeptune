namespace AduabaNeptune.Helper
{
    public class Filter
    {
        //Sets default filter parameter and marks page size limits (200)
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Filter()
        {
            this.PageNumber = 1;
            this.PageSize = 200;
        }
        public Filter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 200 ? 200 : pageSize;
        }
    }
}