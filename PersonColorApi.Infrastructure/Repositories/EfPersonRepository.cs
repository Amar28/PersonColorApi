using Microsoft.EntityFrameworkCore;
using PersonColorApi.Domain.Entities;
using PersonColorApi.Domain.Interfaces;
using PersonColorApi.Infrastructure.Ef;

namespace PersonColorApi.Infrastructure.Repositories
{
    public class EfPersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _db;

        public EfPersonRepository(PersonDbContext db) => _db = db;

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _db.Person.OrderBy(p => p.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _db.Person.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(string color)
        {
            return await _db.Person
                .AsNoTracking()
                .Where(p => p.Color != null && p.Color.ToLower() == color.ToLower())
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Person> AddAsync(Person person)
        {
            var entity = new Person
            {
                LastName = person.LastName,
                Name = person.Name,
                ZipCode = person.ZipCode,
                City = person.City,
                Color = person.Color
            };

            _db.Person.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}