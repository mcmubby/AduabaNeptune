using System;

namespace AduabaNeptune.Dto
{
    public class GetProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string ShopName { get; set; }
        public int ShopId { get; set; }
        public DateTime DateAdded { get; set; }
        public string ProductImage { get; set; }
        public bool Featured { get; set; }
        public int ViewCount { get; set; }
        public bool InStock { get; set; }
        public string ShortDescription { get; set; }
    }
}