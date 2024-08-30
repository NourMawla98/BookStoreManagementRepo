namespace BookStoreManagement.Domain.DTOs
{
    public class GetBookPublisherDTO
    {
        // The ID of the associated book
        public int BookId { get; set; }

        // The title of the associated book (optional but useful for retrieval)
        public string? BookTitle { get; set; }

        // The ID of the associated publisher
        public int PublisherId { get; set; }

        // The name of the associated publisher (optional but useful for retrieval)
        public string? PublisherName { get; set; }
    }
}
