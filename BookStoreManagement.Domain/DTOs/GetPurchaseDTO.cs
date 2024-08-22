namespace BookStoreManagement.Domain.DTOs
{
    using System;


    public class GetPurchaseDTO
    {
        public Guid PurchaseId { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }

}
