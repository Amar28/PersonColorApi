using Application.Persons.DTOs;
using PersonColorApi.Domain.Interfaces;
using MediatR;

namespace Application.Persons.Queries
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonDto>>
    {
        private readonly IPersonRepository _repository;
        public GetAllPersonsQueryHandler(IPersonRepository repository) => _repository = repository;

        public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _repository.GetAllAsync();
            return persons.Select(p => new PersonDto
            {
                Id = p.Id,
                LastName = p.LastName,
                Name = p.Name,
                ZipCode = p.ZipCode,
                City = p.City,
                Color = p.Color
            });
        }
    }
}