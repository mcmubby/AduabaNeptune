using System;

namespace AduabaNeptune.Dto
{
    public class GetProductResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string ShopName { get; set; }
        public int ShopId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}