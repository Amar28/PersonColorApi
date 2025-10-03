using Microsoft.EntityFrameworkCore;
using PersonColorApi.Domain.Entities;

namespace PersonColorApi.Infrastructure.Ef
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; } = null!;
    }

}