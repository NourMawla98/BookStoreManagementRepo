using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{

    public interface IPublisherService
    {
        // Retrieve a list of all publishers
        Task<IEnumerable<GetPublisherDTO>> GetPublishersAsync();

        // Retrieve a specific publisher by ID
        Task<GetPublisherDTO> GetPublisherByIdAsync(int id);

        // Add a new publisher
        Task<GetPublisherDTO> AddPublisherAsync(AddPublisherDTO publisherDto);

        // Update an existing publisher
        Task<bool> UpdatePublisherAsync(GetPublisherDTO publisherDto);

        // Delete a publisher by ID
        Task<bool> DeletePublisherAsync(int id);

    }

}