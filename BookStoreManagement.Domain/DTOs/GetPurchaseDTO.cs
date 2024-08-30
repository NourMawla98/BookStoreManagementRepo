namespace BookStoreManagement.Domain.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GetPurchaseDTO
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal TotalPrice { get; set; }
      
    }


}
