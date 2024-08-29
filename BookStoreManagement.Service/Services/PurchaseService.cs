using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;

using Microsoft.EntityFrameworkCore;
using System;


namespace BookStoreManagement.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IBookStoreRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IBookStoreRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public async Task<bool> PurchaseABook(AddPurchaseDTO purchaseDTO)
        {
            // Map AddPurchaseDTO to Purchase entity
            var newPurchase = _mapper.Map<Purchase>(purchaseDTO);

            // Add the Purchase to the repository
            _purchaseRepository.Add(newPurchase);

            // Save changes to the database
            await _purchaseRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetPurchaseDTO>> TotalSalesPerMonth(int month)
        {
            // Retrieve and map the purchases for the specified month
            return await _purchaseRepository.GetAll<Purchase>()
                .Include(p => p.Book) // Include related Book entity if necessary
                .Where(p => p.PurchaseDate.Month == month)
                .ProjectTo<GetPurchaseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
