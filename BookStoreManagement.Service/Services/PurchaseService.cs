using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Service.Interfaces;
using BookStoreManagement.Service.Repository;

using Microsoft.EntityFrameworkCore;

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
            var book = await _purchaseRepository.GetAll<Book>()
                .AsNoTracking()
                .Where(b => b.Id == purchaseDTO.BookId)
                .Include(b => b.Publishers) // Include all publishers
                .ThenInclude(bp => bp.Publisher) // Include the publisher details
                .FirstOrDefaultAsync();

            if (book == null)
            {
                // Handle the case when the book is not found
                throw new Exception($"Book with Id {purchaseDTO.BookId} not found.");
            }

            // Ensure that there is at least one publisher associated with the book
            var publisher = book.Publishers.FirstOrDefault();
            if (publisher == null)
            {
                throw new Exception($"No publisher found for the book with Id {purchaseDTO.BookId}.");
            }

            // Map AddPurchaseDTO to Purchase entity
            var newPurchase = new Purchase
            {
                BookId = book.Id,
                Bookprice = publisher.Price,
                Quantity = purchaseDTO.Quantity,
                PurchaseDate = DateTime.Now // Set the current date and time
            };

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