using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BookStoreManagement.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IBookStoreRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseService(IBookStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPurchaseDTO>> GetPurchasesAsync()
        {
            // Use ProjectTo to project Purchase entities to GetPurchaseDTO
            return await _repository.GetAll<Purchase>()
                .ProjectTo<GetPurchaseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetPurchaseDTO> GetPurchaseByIdAsync(Guid purchaseId)
        {
            // Use ProjectTo to project the purchase entity to GetPurchaseDTO
            var purchaseDto = await _repository.GetAll<Purchase>()
                .Where(p => p.PurchaseId == purchaseId)
                .ProjectTo<GetPurchaseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ??
                throw new BadHttpRequestException("Purchase not found", (int)HttpStatusCode.NotFound);

            return purchaseDto;
        }

        public async Task<GetPurchaseDTO> AddPurchaseAsync(AddPurchaseDTO purchaseDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            _repository.Add(purchase);
            await _repository.SaveChangesAsync();

            // Use ProjectTo to map the newly added purchase to GetPurchaseDTO
            return await _repository.GetAll<Purchase>()
                .Where(p => p.PurchaseId == purchase.PurchaseId)
                .ProjectTo<GetPurchaseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePurchaseAsync(Guid purchaseId)
        {
            var purchase = await _repository.GetAll<Purchase>()
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId) ??
                throw new BadHttpRequestException("Purchase not found", (int)HttpStatusCode.NotFound);

            _repository.Remove(purchase);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
