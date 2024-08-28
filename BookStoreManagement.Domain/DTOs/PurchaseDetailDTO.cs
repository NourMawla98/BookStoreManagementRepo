using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class PurchaseDetailDTO
    {
        public Guid? PurchaseDetailId { get; set; } // Nullable for AddPurchaseDTO

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }
}
