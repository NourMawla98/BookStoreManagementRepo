using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class AddAuthorDTO
    {
        [Required, MinLength(1)]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}