using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;


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

        // POST: api/Purchase
        [HttpPost]
        public async Task<IActionResult> PurchaseABook([FromBody] AddPurchaseDTO addPurchaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _purchaseService.PurchaseABook(addPurchaseDTO);
            if (!result)
            {
                return BadRequest("Purchase could not be processed.");
            }

            return Ok("Purchase successfully processed.");
        }

        // GET: api/Purchase/monthly-sales
        [HttpGet("monthly-sales")]
        public async Task<IActionResult> GetTotalSalesPerMonth([FromQuery] int month)
        {
            if (month == default)
            {
                return BadRequest("Invalid date.");
            }

            var purchases = await _purchaseService.TotalSalesPerMonth(month);
            if (purchases == null || purchases.Count == 0)
            {
                return NotFound("No purchases found for the specified month.");
            }

            return Ok(purchases);
        }
    }
}
