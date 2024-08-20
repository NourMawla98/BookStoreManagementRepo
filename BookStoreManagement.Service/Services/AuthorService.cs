using AutoMapper;
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

        public async Task<IEnumerable<GetAuthorDTO>> GetAuthorsAsync()
        {
            var authors = await _authorRepository.GetAll<Author>().ToListAsync();
            return _mapper.Map<IEnumerable<GetAuthorDTO>>(authors);
        }

        public async Task<GetAuthorDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAll<Author>().FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<GetAuthorDTO>(author);
        }

        public async Task<GetAuthorDTO> AddAuthorAsync(AddAuthorDTO authorDto)
        {
            var newAuthor = _mapper.Map<Author>(authorDto);
            _authorRepository.Add(newAuthor);
            await _authorRepository.SaveChangesAsync();
            return _mapper.Map<GetAuthorDTO>(newAuthor);
        }

        public async Task<bool> UpdateAuthorAsync(GetAuthorDTO authorDto)
        {
            var author = await _authorRepository.GetAll<Author>().FirstOrDefaultAsync(a => a.Id == authorDto.Id) ??
                throw new BadHttpRequestException("Author not found", (int)HttpStatusCode.NotFound);
            _mapper.Map(authorDto, author);
            await _authorRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetAll<Author>().FirstOrDefaultAsync(a => a.Id == id) ??
                throw new BadHttpRequestException("Author not found", (int)HttpStatusCode.NotFound);

            _authorRepository.Remove(author);
            await _authorRepository.SaveChangesAsync();
            return true;
        }
    }
}