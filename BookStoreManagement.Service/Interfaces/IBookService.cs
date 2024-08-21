using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IAuthorService
    {
        // Retrieve a list of all authors
        Task<IEnumerable<GetAuthorDTO>> GetAuthorsAsync();

        // Retrieve a specific author by ID
        Task<GetAuthorDTO> GetAuthorByIdAsync(int id);

        // Add a new author
        Task<GetAuthorDTO> AddAuthorAsync(AddAuthorDTO authorDto);

        // Update an existing author
        Task<bool> UpdateAuthorAsync(GetAuthorDTO authorDto);

        // Delete an author by ID
        Task<bool> DeleteAuthorAsync(int id);
    }
}