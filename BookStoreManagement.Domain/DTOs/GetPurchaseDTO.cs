namespace BookStoreManagement.Domain.DTOs
{
    using System;



    public class GetPurchaseDTO
    {
        public Guid PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<PurchaseDetailDTO> PurchaseDetails { get; set; } = new List<PurchaseDetailDTO>();
    }


}
