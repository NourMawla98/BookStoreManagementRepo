using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<bool> PurchaseABook(AddPurchaseDTO purchaseDTO);

        Task<List<GetPurchaseDTO>> TotalSalesPerMonth(DateTime date);
    }
}
