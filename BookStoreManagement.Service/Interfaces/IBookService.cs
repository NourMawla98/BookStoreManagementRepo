using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<GetPublisherBookDTO>> GetBooksAsync();
        Task<GetBookDTO> GetBookByIdAsync(int id);
        Task<GetPublisherBookDTO> AddBookAsync(AddBookDTO bookDto);
        Task<bool> UpdateBookAsync(GetPublisherBookDTO bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}