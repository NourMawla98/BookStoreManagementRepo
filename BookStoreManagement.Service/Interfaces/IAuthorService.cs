using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IAuthorService
    {
        // Retrieve a list of all authors
        Task<IEnumerable<AuthorDto>> GetAuthorsAsync();

        // Retrieve a specific author by ID
        Task<AuthorDto> GetAuthorByIdAsync(int id);

        // Add a new author
        Task AddAuthorAsync(AuthorDto authorDto);

        // Update an existing author
        Task UpdateAuthorAsync(AuthorDto authorDto);

        // Delete an author by ID
        Task DeleteAuthorAsync(int id);
    }
}
