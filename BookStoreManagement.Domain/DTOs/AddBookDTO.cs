using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{
   
        public class AddBookDTO
        {
            // The title of the book (required)
            public string Title { get; set; } = String.Empty;

            // The ID of the author associated with the book
            public int AuthorId { get; set; }

            // Optionally, you can add publishers here, if needed
            public List<int> PublisherIds { get; set; } = new List<int>();
        }
    

}