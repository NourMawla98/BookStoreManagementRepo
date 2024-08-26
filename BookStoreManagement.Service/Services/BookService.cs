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

        public async Task<IEnumerable<GetPublisherBookDTO>> GetBooksAsync()
        {
            var books = await _bookRepository.GetAll<Book>().ToListAsync();
            return _mapper.Map<IEnumerable<GetPublisherBookDTO>>(books);
        }

        public async Task<GetPublisherBookDTO> GetBookByIdAsync(int id)
        {
            // Fetch the book including related author and publishers
            var book = await _bookRepository.GetAll<Book>()
                .Include(b => b.Author) // Include the Author
                .Include(b => b.Publishers) // Include BookPublishers to get Publisher details
                    .ThenInclude(bp => bp.Publisher) // Include the Publisher details
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new BadHttpRequestException("Book not found", (int)HttpStatusCode.NotFound);
            }

            // Map the book entity to GetBookDTO
            var bookDto = _mapper.Map<GetPublisherBookDTO>(book);

            // Set the AuthorName and PublisherNames in the DTO
            bookDto.AuthorName = book.Author?.Name; // Assuming the Author has a Name property
            bookDto.PublisherNames = book.Publishers
                                       .Select(bp => bp.Publisher.Name) // Assuming Publisher has a Name property
                                       .ToList();

            return bookDto;
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

                    _bookRepository.Add(bookPublisher); //adding to repo
                }

                // Save changes for the BookPublisher associations
                await _bookRepository.SaveChangesAsync();
            }

            // Map the result to GetBookDTO
            return _mapper.Map<GetPublisherBookDTO>(newBook);
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