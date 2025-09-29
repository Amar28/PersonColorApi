using Application.Persons.DTOs;
using MediatR;

namespace Application.Persons.Commands
{
    public record CreatePersonCommand(string LastName, string Name, string ZipCode, string City, string Color)
        : IRequest<PersonDto>;
}
