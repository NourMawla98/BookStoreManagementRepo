using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class BookPuplisherDTO
    {
        [Range(1, int.MaxValue)]
        public int PublisherId { get; set; }

        [Range(0.01, Double.PositiveInfinity)]
        public decimal Price { get; set; }
    }
}