using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IBookService
    {
        // Retrieve a list of all books
        Task<IEnumerable<GetPublisherDTO>> GetBooksAsync();

        // Retrieve a specific book by ID
        Task<GetPublisherDTO> GetBookByIdAsync(int id);

        // Add a new book
        Task<GetPublisherDTO> AddBookAsync(AddBookDTO bookDto);

        // Update an existing book
        Task<bool> UpdateBookAsync(GetBookDTO bookDto);

        // Delete a book by ID
        Task<bool> DeleteBookAsync(int id);
    }
}