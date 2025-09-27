using Application.Persons.DTOs;
using PersonColorApi.Domain.Interfaces;
using MediatR;

namespace Application.Persons.Queries
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto?>
    {
        private readonly IPersonRepository _repository;
        public GetPersonByIdQueryHandler(IPersonRepository repository) => _repository = repository;

        public async Task<PersonDto?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.Id);
            if (person == null) return null;
            return new PersonDto
            {
                Id = person.Id,
                LastName = person.LastName,
                Name = person.Name,
                ZipCode = person.ZipCode,
                City = person.City,
                Color = person.Color
            };
        }
    }
}