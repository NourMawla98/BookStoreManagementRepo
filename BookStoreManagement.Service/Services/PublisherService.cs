using AutoMapper;
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

        public async Task<IEnumerable<GetPublisherDTO>> GetPublishersAsync()
        {
            var publishers = await _publisherRepository.GetAll<Publisher>().ToListAsync();
            return _mapper.Map<IEnumerable<GetPublisherDTO>>(publishers);
        }

        public async Task<GetPublisherDTO> GetPublisherByIdAsync(int id)
        {
            var publisher = await _publisherRepository.GetAll<Publisher>().FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<GetPublisherDTO>(publisher);
        }

        public async Task<GetPublisherDTO> AddPublisherAsync(AddPublisherDTO publisherDto)
        {
            var newPublisher = _mapper.Map<Publisher>(publisherDto);
            _publisherRepository.Add(newPublisher);
            await _publisherRepository.SaveChangesAsync();
            return _mapper.Map<GetPublisherDTO>(newPublisher);
        }

        public async Task<bool> UpdatePublisherAsync(GetPublisherDTO publisherDto)
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