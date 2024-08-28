using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreManagement.Domain.Context;
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
        private readonly IMapper _mapper;
        private readonly BookStoreDBContext _context;

        public PurchaseService(IMapper mapper, BookStoreDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Purchase> AddPurchaseAsync(AddPurchaseDTO addPurchaseDTO)
        {
            var purchase = _mapper.Map<Purchase>(addPurchaseDTO);
            // Calculate TotalPrice based on PurchaseDetails
            purchase.TotalPrice = purchase.PurchaseDetails.Sum(pd => pd.Price * pd.Quantity);

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task<GetPurchaseDTO> GetPurchaseByIdAsync(Guid id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .SingleOrDefaultAsync(p => p.PurchaseId == id);

            return purchase != null ? _mapper.Map<GetPurchaseDTO>(purchase) : null;
        }

        public async Task<IEnumerable<GetPurchaseDTO>> GetAllPurchasesAsync()
        {
            var purchases = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetPurchaseDTO>>(purchases);
        }

        public async Task<GetPurchaseDTO> UpdatePurchaseAsync(Guid id, AddPurchaseDTO updatePurchaseDTO)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .SingleOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null) return null;

            // Update purchase details
            _mapper.Map(updatePurchaseDTO, purchase);

            // Recalculate TotalPrice
            purchase.TotalPrice = purchase.PurchaseDetails.Sum(pd => pd.Price * pd.Quantity);

            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetPurchaseDTO>(purchase);
        }

        public async Task<bool> DeletePurchaseAsync(Guid id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .SingleOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null) return false;

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
