using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.Business;
using RestAPI.Data.VOs;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBusiness<BookVO> _business;

        public BookController(ILogger<BookController> logger, IBusiness<BookVO> business)
        {
            _logger = logger;
            _business = business;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_business.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return BadRequest();

            var book = _business.FindById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();

            var result = _business.Create(book);

            if (result == null)
                return BadRequest("Couldn't create the record. Invalid inputs.");

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();

            var result = _business.Update(book);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _business.Delete(id);

            return NoContent();
        }
    }
}