using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManagement.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            var authors =   _authorRepository.GetAll().ToList();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAll().FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task AddAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            _authorRepository.Add(author);
            await _authorRepository.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = await _authorRepository.GetAll().FirstOrDefaultAsync(a => a.Id == authorDto.Id);
            if (author != null)
            {
                _mapper.Map(authorDto, author);
                await _authorRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetAll().FirstOrDefaultAsync(a => a.Id == id);
            if (author != null)
            {
                _authorRepository.Remove(author);
                await _authorRepository.SaveChangesAsync();
            }
        }
    }
}
