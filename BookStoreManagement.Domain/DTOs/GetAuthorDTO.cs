namespace BookStoreManagement.Domain.DTOs
{
    public class GetAuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // This will hold a list of books associated with the author
        public List<GetPublisherBookDTO> Books { get; set; }
    }

}
