using Application.Persons.DTOs;
using Application.Persons.Queries;
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

        [HttpGet]
        public async Task<IEnumerable<PersonDto>> GetAll() => await _mediator.Send(new GetAllPersonsQuery());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            return person is null ? NotFound() : Ok(person);
        }

        [HttpGet("color/{color}")]
        public async Task<IEnumerable<PersonDto>> GetByColor(string color) => await _mediator.Send(new GetPersonsByColorQuery(color));
    }
}