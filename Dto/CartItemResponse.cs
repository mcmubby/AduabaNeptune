namespace AduabaNeptune.Dto
{
    public class CartItemResponse
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductUnitPrice { get; set; }
        public int TotalQuantityAvailable { get; set; }
        public int QuantityOfProductInCart { get; set; }
        public string ProductImage { get; set; }
        public int CartId { get; set; }
    }
}