using BookStoreManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagement.Domain.DTOs
{

    public class GetPublisherListDTO
    {
        // The unique identifier of the publisher
        public int Id { get; set; }

        // The name of the publisher
        public string? Name { get; set; }
      
    }


}