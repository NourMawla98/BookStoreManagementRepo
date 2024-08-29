using BookStoreManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{

    public class AddBookDTO
    {
        // The title of the book (required)
        public string Title { get; set; } = String.Empty;

        public BookGenreEnum Genre { get; set; }

        // The ID of the author associated with the book
        public int AuthorId { get; set; }

        // Optionally, you can add publishers here, if needed
        public List<AddPublisherDTO> Publishers { get; set; } = new List<AddPublisherDTO>();

    }


}