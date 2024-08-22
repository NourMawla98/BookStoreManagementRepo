using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<GetAuthorDTO>> GetAuthorsAsync();
        Task<GetAuthorDTO> GetAuthorByIdAsync(int id);
        Task<GetAuthorDTO> AddAuthorAsync(AddAuthorDTO authorDto);
        Task<bool> UpdateAuthorAsync(GetAuthorDTO authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}