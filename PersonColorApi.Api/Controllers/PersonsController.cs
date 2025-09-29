using Application.Persons.DTOs;
using Application.Persons.Queries;
using Application.Persons.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonCommand command)
        {
            var person = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {

            var persons = await _mediator.Send(new GetAllPersonsQuery());

            return Ok(persons);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            return person is null ? NotFound() : Ok(person);
        }

        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetByColor(string color)
        {
            var person = await _mediator.Send(new GetPersonsByColorQuery(color));

            return Ok(person);
        }
    }
}