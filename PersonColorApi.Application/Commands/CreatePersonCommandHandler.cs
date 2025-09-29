using Application.Persons.DTOs;
using PersonColorApi.Domain.Entities;
using PersonColorApi.Domain.Interfaces;
using MediatR;

namespace Application.Persons.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
    {
        private readonly IPersonRepository _repository;
        public CreatePersonCommandHandler(IPersonRepository repository) => _repository = repository;

        public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                LastName = request.LastName,
                Name = request.Name,
                ZipCode = request.ZipCode,
                City = request.City,
                Color = request.Color
            };

            var created = await _repository.AddAsync(person);

            return new PersonDto
            {
                Id = created.Id,
                LastName = created.LastName,
                Name = created.Name,
                ZipCode = created.ZipCode,
                City = created.City,
                Color = created.Color
            };
        }
    }
}