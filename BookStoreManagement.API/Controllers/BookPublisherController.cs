using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPublisherController : ControllerBase
    {
        private readonly IBookPublisherService _bookPublisherService;

        public BookPublisherController(IBookPublisherService bookPublisherService)
        {
            _bookPublisherService = bookPublisherService;
        }

        // GET: api/BookPublisher
        [HttpGet]
        public async Task<IActionResult> GetBookPublishers()
        {
            return Ok(await _bookPublisherService.GetBookPublishersAsync());
        }

        // GET: api/BookPublisher/BookId/PublisherId
        [HttpGet("{bookId:int}/{publisherId:int}")]
        public async Task<IActionResult> GetBookPublisher(int bookId, int publisherId)
        {
            return Ok(await _bookPublisherService.GetBookPublisherAsync(bookId, publisherId));
        }

        // POST: api/BookPublisher
        [HttpPost]
        public async Task<IActionResult> AddBookPublisher([FromBody] AddBookPublisherDTO bookPublisherDto)
        {
            return Ok(await _bookPublisherService.AddBookPublisherAsync(bookPublisherDto));
        }

        // DELETE: api/BookPublisher/BookId/PublisherId
        [HttpDelete("{bookId:int}/{publisherId:int}")]
        public async Task<IActionResult> DeleteBookPublisher(int bookId, int publisherId)
        {
            return Ok(await _bookPublisherService.DeleteBookPublisherAsync(bookId, publisherId));
        }
    }
}
