using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchases()
        {
            var purchases = await _purchaseService.GetPurchasesAsync();
            return Ok(purchases);
        }

        [HttpGet("{purchaseId}")]
        public async Task<IActionResult> GetPurchase(Guid purchaseId)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(purchaseId);
            return purchase != null ? Ok(purchase) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] AddPurchaseDTO purchaseDto)
        {
            var newPurchase = await _purchaseService.AddPurchaseAsync(purchaseDto);
            return CreatedAtAction(nameof(GetPurchase), new { purchaseId = newPurchase.PurchaseId }, newPurchase);
        }

        [HttpDelete("{purchaseId}")]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            var result = await _purchaseService.DeletePurchaseAsync(purchaseId);
            return result ? NoContent() : NotFound();
        }
    }
}
