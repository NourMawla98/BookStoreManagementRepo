using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: api/Publisher
        // GET: api/Publisher
        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
          
            return Ok(await _publisherService.GetPublishersAsync());
        }

        
        // GET: api/Publisher/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            return Ok(publisher);
        }


        // POST: api/Publisher
        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] AddPublisherDTO publisherDto)
        {
            return Ok(await _publisherService.AddPublisherAsync(publisherDto));
        }

        // PUT: api/Publisher/5
        [HttpPut]
        public async Task<IActionResult> UpdatePublisher([FromBody] GetPublisherListDTO publisherDto)
        {
            return Ok(await _publisherService.UpdatePublisherAsync(publisherDto));
        }

        // DELETE: api/Publisher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            return Ok(await _publisherService.DeletePublisherAsync(id));
        }
    }
}
