using BookStoreManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<GetPurchaseDTO>> GetPurchasesAsync();
        Task<GetPurchaseDTO> GetPurchaseByIdAsync(Guid purchaseId);
        Task<GetPurchaseDTO> AddPurchaseAsync(AddPurchaseDTO purchaseDto);
        Task<bool> DeletePurchaseAsync(Guid purchaseId);
    }
}
