using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _authorService.GetAuthorsAsync());
        }


        // GET: api/Author/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
        }


        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorDTO authorDto)
        {
            return Ok(await _authorService.AddAuthorAsync(authorDto));
        }

        // PUT: api/Author/5
        [HttpPut]
        public async Task<IActionResult> PutAuthor([FromBody] GetAuthorListDTO authorDto)
        {
            return Ok(await _authorService.UpdateAuthorAsync(authorDto));
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _authorService.DeleteAuthorAsync(id));
        }
    }
}