using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class AddPublisherDTO
    {
        [Required, MinLength(1), MaxLength(250)]
        public string Name { get; set; }

        [Required, MinLength(1), MaxLength(250)]
        public string Address { get; set; }
    }
}