using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<GetAuthorListDTO>> GetAuthorsAsync();
        Task<GetAuthorDTO> GetAuthorByIdAsync(int id);
      
        Task<GetAuthorListDTO> AddAuthorAsync(AddAuthorDTO authorDto);
        Task<bool> UpdateAuthorAsync(GetAuthorListDTO authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}