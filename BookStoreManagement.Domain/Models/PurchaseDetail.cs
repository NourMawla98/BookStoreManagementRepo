using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.Models
{
    public class PurchaseDetail
    {
        [Key]
        public Guid PurchaseDetailId { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public Guid PurchaseId { get; set; }

        // Navigation properties
        public Purchase Purchase { get; set; }
    }
}
