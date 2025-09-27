using Application.Persons.DTOs;
using MediatR;

namespace Application.Persons.Queries
{
    public record GetPersonByIdQuery(int Id) : IRequest<PersonDto?>;
}