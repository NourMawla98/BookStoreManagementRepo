

namespace BookStoreManagement.Domain.DTOs
{
    public class GetPublisherBookDTO
    {
        // The unique identifier of the book
        public int Id { get; set; }

        // The title of the book
        public string Title { get; set; } = String.Empty;
        public string Genre { get; set; }

        // The ID of the author associated with the book
        public int AuthorId { get; set; }

        // The name of the author associated with the book
        public string AuthorName { get; set; } = String.Empty;
    }
}


