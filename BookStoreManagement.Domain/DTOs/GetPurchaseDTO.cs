namespace BookStoreManagement.Domain.DTOs
{
    using System;



    public class GetPurchaseDTO
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
      
    }


}
