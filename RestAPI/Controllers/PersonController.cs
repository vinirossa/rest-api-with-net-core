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
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IBusiness<PersonVO> _business;

        public PersonController(ILogger<PersonController> logger, IBusiness<PersonVO> business)
        {
            _logger = logger;
            _business = business;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_business.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            if (id == 0)
                return BadRequest();

            var person = _business.FindById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();

            var result = _business.Create(person);

            if (result == null)
                return BadRequest("Couldn't create the record. Invalid inputs.");

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();

            var result = _business.Update(person);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _business.Delete(id);

            return NoContent();
        }
    }
}