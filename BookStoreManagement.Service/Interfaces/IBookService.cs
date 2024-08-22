using BookStoreManagement.Domain.DTOs;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<GetPublisherDTO>> GetBooksAsync();
        Task<GetPublisherDTO> GetBookByIdAsync(int id);
        Task<GetPublisherDTO> AddBookAsync(AddBookDTO bookDto);
        Task<bool> UpdateBookAsync(GetBookDTO bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}