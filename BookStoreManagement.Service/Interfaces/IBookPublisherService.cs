using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IBookPublisherService
    {
        // Retrieve a list of all book-publisher relationships
        Task<IEnumerable<GetBookPublisherDTO>> GetBookPublishersAsync();

        // Retrieve a specific book-publisher relationship by BookId and PublisherId
        Task<GetBookPublisherDTO> GetBookPublisherAsync(int bookId, int publisherId);

        // Add a new book-publisher relationship
        Task<GetBookPublisherDTO> AddBookPublisherAsync(AddBookPublisherDTO bookPublisherDto);

        // Delete a specific book-publisher relationship by BookId and PublisherId
        Task<bool> DeleteBookPublisherAsync(int bookId, int publisherId);
    }
}