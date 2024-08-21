namespace BookStoreManagement.Domain.DTOs
{
    public class AddBookPublisherDTO
    {
        // The ID of the book to be associated with the publisher
        public int BookId { get; set; }

        // The ID of the publisher to be associated with the book
        public int PublisherId { get; set; }
    }
}

