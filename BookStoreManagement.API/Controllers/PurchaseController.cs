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

        // POST: api/Purchase
        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] AddPurchaseDTO addPurchaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchase = await _purchaseService.AddPurchaseAsync(addPurchaseDTO);
            if (purchase == null)
            {
                return BadRequest("Purchase could not be created.");
            }

            return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.PurchaseId }, purchase);
        }

        // GET: api/Purchase/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(Guid id)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // GET: api/Purchase
        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetAllPurchasesAsync();
            return Ok(purchases);
        }

        // PUT: api/Purchase/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(Guid id, [FromBody] AddPurchaseDTO updatePurchaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPurchase = await _purchaseService.UpdatePurchaseAsync(id, updatePurchaseDTO);
            if (updatedPurchase == null)
            {
                return NotFound();
            }

            return Ok(updatedPurchase); // Return the updated purchase details
        }

        // DELETE: api/Purchase/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            var isDeleted = await _purchaseService.DeletePurchaseAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent(); // 204 status
        }
    }
}
