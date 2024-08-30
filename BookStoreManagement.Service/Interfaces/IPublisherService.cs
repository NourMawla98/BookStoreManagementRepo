using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IPublisherService
    {
        // Retrieve a list of all publishers
        Task<IEnumerable<GetPublisherListDTO>> GetPublishersAsync();

        // Retrieve a specific publisher by ID
        Task<GetPublisherDTO> GetPublisherByIdAsync(int id);

        // Add a new publisher
        Task<GetPublisherListDTO> AddPublisherAsync(AddPublisherDTO publisherDto);

        // Update an existing publisher
        Task<bool> UpdatePublisherAsync(GetPublisherListDTO publisherDto);

        // Delete a publisher by ID
        Task<bool> DeletePublisherAsync(int id);


    }
}