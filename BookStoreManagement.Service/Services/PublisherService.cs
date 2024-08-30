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
    public class PublisherService : IPublisherService
    {
        private readonly IBookStoreRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherService(IBookStoreRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<GetPublisherListDTO> AddPublisherAsync(AddPublisherDTO publisherDto)
        {
            // Map AddPublisherDTO to Publisher entity
            var newPublisher = _mapper.Map<Publisher>(publisherDto);

            // Add the Publisher to the repository
            _publisherRepository.Add(newPublisher);

            // Save changes to the database
            await _publisherRepository.SaveChangesAsync();

            return await _publisherRepository.GetAll<Publisher>()
                  .Where(p => p.Id == newPublisher.Id)
                  .ProjectTo<GetPublisherListDTO>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync();


        }


        public async Task<IEnumerable<GetPublisherListDTO>> GetPublishersAsync()
        {
            return await _publisherRepository.GetAll<Publisher>()
                .ProjectTo<GetPublisherListDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetPublisherDTO> GetPublisherByIdAsync(int id)
        {
            var publisherDTO = await _publisherRepository.GetAll<Publisher>()
                .Where(p => p.Id == id)
                .ProjectTo<GetPublisherDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ??
                throw new BadHttpRequestException("Publisher not found", (int)HttpStatusCode.NotFound);

            return publisherDTO;
        }

        public async Task<bool> UpdatePublisherAsync(GetPublisherListDTO publisherDto)
        {
            var publisher = await _publisherRepository.GetAll<Publisher>().FirstOrDefaultAsync(p => p.Id == publisherDto.Id) ??
                throw new BadHttpRequestException("Publisher not found", (int)HttpStatusCode.NotFound);

            _mapper.Map(publisherDto, publisher);
            await _publisherRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _publisherRepository.GetAll<Publisher>().FirstOrDefaultAsync(p => p.Id == id) ??
                throw new BadHttpRequestException("Publisher not found", (int)HttpStatusCode.NotFound);

            _publisherRepository.Remove(publisher);
            await _publisherRepository.SaveChangesAsync();
            return true;
        }
    }
}