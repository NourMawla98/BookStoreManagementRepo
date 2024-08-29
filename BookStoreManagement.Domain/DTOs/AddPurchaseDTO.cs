using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class AddPurchaseDTO
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "BookId must be a positive integer.")]
        public int BookId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Book price must be greater than zero.")]
        public int PublisherId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
    }
}