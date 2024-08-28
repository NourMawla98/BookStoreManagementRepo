using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class AddPurchaseDTO
    {
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public List<PurchaseDetailDTO> PurchaseDetails { get; set; } = new List<PurchaseDetailDTO>();
    }
}





