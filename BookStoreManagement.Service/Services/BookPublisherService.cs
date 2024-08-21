using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookStoreManagement.Service.Services
{
    public class BookPublisherService : IBookPublisherService
    {
        private readonly IBookStoreRepository _repository;
        private readonly IMapper _mapper;

        public BookPublisherService(IBookStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBookPublisherDTO>> GetBookPublishersAsync()
        {
            var bookPublishers = await _repository.GetAll<BookPublisher>()
                .Include(bp => bp.Book)
                .Include(bp => bp.Publisher)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetBookPublisherDTO>>(bookPublishers);
        }

        public async Task<GetBookPublisherDTO> GetBookPublisherAsync(int bookId, int publisherId)
        {
            var bookPublisher = await _repository.GetAll<BookPublisher>()
                .Include(bp => bp.Book)
                .Include(bp => bp.Publisher)
                .FirstOrDefaultAsync(bp => bp.BookId == bookId && bp.PublisherId == publisherId);

            if (bookPublisher == null)
                throw new BadHttpRequestException("BookPublisher relationship not found", (int)HttpStatusCode.NotFound);

            return _mapper.Map<GetBookPublisherDTO>(bookPublisher);
        }

        public async Task<GetBookPublisherDTO> AddBookPublisherAsync(AddBookPublisherDTO bookPublisherDto)
        {
            var bookPublisher = _mapper.Map<BookPublisher>(bookPublisherDto);
            _repository.Add(bookPublisher);
            await _repository.SaveChangesAsync();
            return _mapper.Map<GetBookPublisherDTO>(bookPublisher);
        }

        public async Task<bool> DeleteBookPublisherAsync(int bookId, int publisherId)
        {
            var bookPublisher = await _repository.GetAll<BookPublisher>()
                .FirstOrDefaultAsync(bp => bp.BookId == bookId && bp.PublisherId == publisherId);

            if (bookPublisher == null)
                throw new BadHttpRequestException("BookPublisher relationship not found", (int)HttpStatusCode.NotFound);

            _repository.Remove(bookPublisher);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}