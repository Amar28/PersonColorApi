using Application.Persons.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Persons.Queries
{
    public record GetPersonsByColorQuery(string Color) : IRequest<IEnumerable<PersonDto>>;
}
