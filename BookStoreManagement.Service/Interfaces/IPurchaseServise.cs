using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> AddPurchaseAsync(AddPurchaseDTO addPurchaseDTO);
        Task<GetPurchaseDTO> GetPurchaseByIdAsync(Guid id);
        Task<IEnumerable<GetPurchaseDTO>> GetAllPurchasesAsync();
        Task<GetPurchaseDTO> UpdatePurchaseAsync(Guid id, AddPurchaseDTO updatePurchaseDTO);
        Task<bool> DeletePurchaseAsync(Guid id);
    }
}
