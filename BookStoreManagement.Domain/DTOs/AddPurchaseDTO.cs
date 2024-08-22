using System;

namespace BookStoreManagement.Domain.DTOs
{
    public class AddPurchaseDTO
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}





