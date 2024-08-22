using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookStoreManagement.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookStoreRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookStoreRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPublisherDTO>> GetBooksAsync()
        {
            var books = await _bookRepository.GetAll<Book>().ToListAsync();
            return _mapper.Map<IEnumerable<GetPublisherDTO>>(books);
        }

        public async Task<GetPublisherDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetAll<Book>().FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<GetPublisherDTO>(book);
        }

        public async Task<GetPublisherDTO> AddBookAsync(AddBookDTO bookDto)
        {
            var newBook = _mapper.Map<Book>(bookDto);
            _bookRepository.Add(newBook);
            await _bookRepository.SaveChangesAsync();
            return _mapper.Map<GetPublisherDTO>(newBook);
        }

        public async Task<bool> UpdateBookAsync(GetBookDTO bookDto)
        {
            var book = await _bookRepository.GetAll<Book>().FirstOrDefaultAsync(b => b.Id == bookDto.Id) ??
                throw new BadHttpRequestException("Book not found", (int)HttpStatusCode.NotFound);

            _mapper.Map(bookDto, book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetAll<Book>().FirstOrDefaultAsync(b => b.Id == id) ??
                throw new BadHttpRequestException("Book not found", (int)HttpStatusCode.NotFound);

            _bookRepository.Remove(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }
    }
}
