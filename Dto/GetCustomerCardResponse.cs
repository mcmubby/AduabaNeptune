using System;

namespace AduabaNeptune.Dto
{
    public class GetCustomerCardResponse
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CCV { get; set; }
        public string CardId { get; set; }
    }
}