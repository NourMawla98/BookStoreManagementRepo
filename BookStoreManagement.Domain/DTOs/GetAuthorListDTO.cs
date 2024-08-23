using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
    public class GetAuthorListDTO : AddAuthorDTO
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }

        public string Name { get; set; }
      

    }
}