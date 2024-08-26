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
    public class AuthorService : IAuthorService
    {
        private readonly IBookStoreRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IBookStoreRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAuthorListDTO>> GetAuthorsAsync()
        {
            // Use ProjectTo to map authors to GetAuthorListDTO (without books)
            return await _authorRepository.GetAll<Author>()
                .ProjectTo<GetAuthorListDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetAuthorDTO> GetAuthorByIdAsync(int id)
        {
            // Use ProjectTo to map the author with books to GetAuthorDTO
            var authorDTO = await _authorRepository.GetAll<Author>()
                .Where(a => a.Id == id)
                .ProjectTo<GetAuthorDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ??
                throw new BadHttpRequestException("Author not found", (int)HttpStatusCode.NotFound);

            return authorDTO;
        }

        public async Task<GetAuthorListDTO> AddAuthorAsync(AddAuthorDTO authorDto)
        {
            var newAuthor = _mapper.Map<Author>(authorDto);
            _authorRepository.Add(newAuthor);
            await _authorRepository.SaveChangesAsync();

            // Use ProjectTo to map the newly added author to GetAuthorListDTO
            return await _authorRepository.GetAll<Author>()
                .Where(a => a.Id == newAuthor.Id)
                .ProjectTo<GetAuthorListDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAuthorAsync(GetAuthorListDTO authorDto)
        {
            // Find the author by Id
            var author = await _authorRepository.GetAll<Author>()
                .FirstOrDefaultAsync(a => a.Id == authorDto.Id) ??
                throw new BadHttpRequestException("Author not found", (int)HttpStatusCode.NotFound);

            // Map the DTO to the Author entity and save changes
            _mapper.Map(authorDto, author);
            await _authorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            // Find the author by Id
            var author = await _authorRepository.GetAll<Author>()
                .FirstOrDefaultAsync(a => a.Id == id) ??
                throw new BadHttpRequestException("Author not found", (int)HttpStatusCode.NotFound);

            // Remove the author and save changes
            _authorRepository.Remove(author);
            await _authorRepository.SaveChangesAsync();

            return true;
        }
    }
}