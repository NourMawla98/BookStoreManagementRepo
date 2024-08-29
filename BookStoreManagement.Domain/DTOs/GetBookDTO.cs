using BookStoreManagement.Domain.Enums;

namespace BookStoreManagement.Domain.DTOs
{
    public class GetBookDTO
    {
        public int Id { get; set; }

        // Title of the book
        public string Title { get; set; } = string.Empty;

        // The genre of the book
        public BookGenreEnum Genre { get; set; }

        // Author details
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        // List of publishers for this book
        public List<GetPublisherListDTO> Publishers { get; set; } = new List<GetPublisherListDTO>();
    }
}
