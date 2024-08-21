using BookStoreManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{

    public class GetPublisherDTO
    {
        // The unique identifier of the publisher
        public int Id { get; set; }

        // The name of the publisher
        public string? Name { get; set; }

        // The address of the publisher
        public string? Address { get; set; }

      
    }


}