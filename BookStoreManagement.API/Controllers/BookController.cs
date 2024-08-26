using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        // GET: api/Book/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _bookService.GetBookByIdAsync(id));
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDTO bookDto)
        {
            return Ok(await _bookService.AddBookAsync(bookDto));
        }

        // PUT: api/Book/5
        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] GetPublisherBookDTO bookDto)
        {
            return Ok(await _bookService.UpdateBookAsync(bookDto));
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _bookService.DeleteBookAsync(id));
        }
    }
}
