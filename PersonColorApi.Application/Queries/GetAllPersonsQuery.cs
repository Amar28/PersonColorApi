using Application.Persons.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Persons.Queries
{
    public record GetAllPersonsQuery() : IRequest<IEnumerable<PersonDto>>;
}