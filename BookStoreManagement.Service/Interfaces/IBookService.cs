using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<GetBookDTO>> GetBooksAsync();
        Task<GetBookDTO> GetBookByIdAsync(int id);
        Task<GetBookDTO> AddBookAsync(AddBookDTO bookDto);
        Task<bool> UpdateBookAsync(GetBookDTO bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}