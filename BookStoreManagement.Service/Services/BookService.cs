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
        private readonly IBookStoreRepository _publisherRepository;
        private readonly IMapper _mapper;

        public BookService(IBookStoreRepository bookRepository, IBookStoreRepository publisherRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
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
            var book = await _bookRepository.GetAll<Book>()
                .Include(b => b.Publishers)
                    .ThenInclude(bp => bp.Publisher) // Ensure you still include the publishers
                .FirstOrDefaultAsync(b => b.Id == id);

            // Manually map to GetBookDTO if ProjectTo is causing issues
            return _mapper.Map<GetBookDTO>(book);
        }




        public async Task<GetPublisherBookDTO> AddBookAsync(AddBookDTO bookDto)
        {
            // Map DTO to Book entity
            var newBook = _mapper.Map<Book>(bookDto);

            // Add the book to the repository
            _bookRepository.Add(newBook);
            await _bookRepository.SaveChangesAsync();

            // After saving the book, associate it with publishers
            if (bookDto.Publishers != null && bookDto.Publishers.Any())
            {
                foreach (var publisherDto in bookDto.Publishers)
                {
                    // Check if publisher exists by name and address
                    var existingPublisher = await _publisherRepository.GetAll<Publisher>()
                        .FirstOrDefaultAsync(p => p.Name == publisherDto.Name && p.Location == publisherDto.Address);

                    // If the publisher doesn't exist, create it
                    if (existingPublisher == null)
                    {
                        var newPublisher = _mapper.Map<Publisher>(publisherDto);
                        _publisherRepository.Add(newPublisher);
                        await _publisherRepository.SaveChangesAsync();
                        existingPublisher = newPublisher;
                    }

                    // Create a new BookPublisher entry
                    var bookPublisher = new BookPublisher
                    {
                        BookId = newBook.Id,
                        PublisherId = existingPublisher.Id
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
