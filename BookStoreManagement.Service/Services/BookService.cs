using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<IEnumerable<GetPublisherBookDTO>> GetBooksAsync()
        {
            // Use ProjectTo to project Books to GetPublisherBookDTO
            return await _bookRepository.GetAll<Book>()
                .ProjectTo<GetPublisherBookDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetBookDTO> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetAll<Book>()
                .Include(b => b.Publishers).ThenInclude(bp => bp.Publisher) // Ensure you still include the publishers
                .ProjectTo<GetBookDTO>(_mapper.ConfigurationProvider) // Project directly to GetBookDTO
                .FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task<GetPublisherBookDTO> AddBookAsync(AddBookDTO bookDto)
        {
            // Map DTO to Book entity
            var newBook = _mapper.Map<Book>(bookDto);

            // Add the book to the repository
            _bookRepository.Add(newBook);
            await _bookRepository.SaveChangesAsync();

            // After saving the book, associate it with publishers
            if (bookDto.PublisherIds != null && bookDto.PublisherIds.Any())
            {
                foreach (var publisherId in bookDto.PublisherIds)
                {
                    // Create a new BookPublisher entry for each publisherId
                    var bookPublisher = new BookPublisher
                    {
                        BookId = newBook.Id,
                        PublisherId = publisherId
                    };

                    _bookRepository.Add(bookPublisher); // Adding to repo
                }

                // Save changes for the BookPublisher associations
                await _bookRepository.SaveChangesAsync();
            }

            // Use ProjectTo to return the newly added book as GetPublisherBookDTO
            return await _bookRepository.GetAll<Book>()
                .Where(b => b.Id == newBook.Id)
                .ProjectTo<GetPublisherBookDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBookAsync(GetPublisherBookDTO bookDto)
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
