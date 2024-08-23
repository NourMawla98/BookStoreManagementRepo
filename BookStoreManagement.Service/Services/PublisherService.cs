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
        public async Task<GetPublisherListDTO> AddPublisherAsync(AddPublisherDTO publisherDto)
        {
            // Map AddPublisherDTO to Publisher entity
            var newPublisher = _mapper.Map<Publisher>(publisherDto);

            // Add the Publisher to the repository
            _publisherRepository.Add(newPublisher);

            // Save changes to the database
            await _publisherRepository.SaveChangesAsync();

            // Map the newly added Publisher back to GetPublisherListDTO
            var publisherDtoResult = _mapper.Map<GetPublisherListDTO>(newPublisher);

            return publisherDtoResult;
        }

        public async Task<IEnumerable<GetPublisherListDTO>> GetPublishersAsync()
        {
            var publishers = await _publisherRepository.GetAll<Publisher>().ToListAsync();
            return _mapper.Map<IEnumerable<GetPublisherListDTO>>(publishers);
        }




        public async Task<GetPublisherDTO> GetPublisherByIdAsync(int id)
        {
            var publisher = await _publisherRepository.GetAll<Publisher>()
                                                      .Include(p => p.PublishedBooks)
                                                      .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                throw new BadHttpRequestException("Publisher not found", (int)HttpStatusCode.NotFound);
            }

            return _mapper.Map<GetPublisherDTO>(publisher);
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