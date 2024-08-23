namespace BookStoreManagement.Domain.DTOs
{
    public class GetPublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetBookDTO> Books { get; set; } // List of books published by this publisher
    }
}

