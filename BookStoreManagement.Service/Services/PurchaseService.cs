using AutoMapper;
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
            var purchases = await _repository.GetAll<Purchase>()
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetPurchaseDTO>>(purchases);
        }

        public async Task<GetPurchaseDTO> GetPurchaseByIdAsync(Guid purchaseId)
        {
            var purchase = await _repository.GetAll<Purchase>()
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

            if (purchase == null)
                throw new BadHttpRequestException("Purchase not found", (int)HttpStatusCode.NotFound);

            return _mapper.Map<GetPurchaseDTO>(purchase);
        }

        public async Task<GetPurchaseDTO> AddPurchaseAsync(AddPurchaseDTO purchaseDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            _repository.Add(purchase);
            await _repository.SaveChangesAsync();
            return _mapper.Map<GetPurchaseDTO>(purchase);
        }

        public async Task<bool> DeletePurchaseAsync(Guid purchaseId)
        {
            var purchase = await _repository.GetAll<Purchase>()
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

            if (purchase == null)
                throw new BadHttpRequestException("Purchase not found", (int)HttpStatusCode.NotFound);

            _repository.Remove(purchase);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
