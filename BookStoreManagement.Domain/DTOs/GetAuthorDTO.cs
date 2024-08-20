using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class GetAuthorDTO : AddAuthorDTO
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}