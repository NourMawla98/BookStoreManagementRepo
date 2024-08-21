using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{

    public class AddPublisherDTO
    {
        // The name of the publisher (required)
        public string? Name { get; set; }

        // The address of the publisher (optional)
        public string? Address { get; set; }


    }


}