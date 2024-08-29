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
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .Include(b => b.Publishers)
                    .ThenInclude(bp => bp.Publisher) // Ensure you still include the publishers
                .FirstOrDefaultAsync();

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

            // Use ProjectTo to return the newly added book as GetPublisherBookDTO
            return _mapper.Map<GetPublisherBookDTO>(newBook);
        }

        public async Task<bool> UpdateBookAsync(EditBookDTO bookDto)
        {
            var book = await _bookRepository.GetAll<Book>()
                .Where(b => b.Id == bookDto.Id)
                .Include(b => b.Publishers)
                .FirstOrDefaultAsync() ??
                throw new BadHttpRequestException("Book not found", (int)HttpStatusCode.NotFound);

            _mapper.Map(bookDto, book);

            if ((book.Publishers == null || book.Publishers.Count == 0) && (bookDto.Publishers != null && bookDto.Publishers.Count > 0))
                book.Publishers = _mapper.Map<List<BookPublisher>>(bookDto.Publishers);
            else
            {
                //Foreach to add missing publisher
                foreach (var dto in bookDto.Publishers)
                {
                    var bp = book.Publishers.FirstOrDefault(p => p.PublisherId == dto.PublisherId);

                    if (bp == null)
                        book.Publishers.Add(_mapper.Map<BookPublisher>(dto));
                    else
                        bp.Price = dto.Price;
                }

                //foreach to delete unwanted publishers
                foreach (var bp in book.Publishers)
                {
                    var dto = bookDto.Publishers.FirstOrDefault(dto => dto.PublisherId == bp.PublisherId);

                    if (dto == null)
                        book.Publishers.Remove(bp);
                }
            }

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