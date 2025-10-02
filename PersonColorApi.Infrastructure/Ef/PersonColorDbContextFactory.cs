using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PersonColorApi.Infrastructure.Ef
{
    public class PersonColorDbContextFactory : IDesignTimeDbContextFactory<PersonDbContext>
    {
        public PersonDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersonDbContext>();

            // hier z. B. lokale Verbindung einstellen
            optionsBuilder.UseSqlite("PersonDb");

            return new PersonDbContext(optionsBuilder.Options);
        }
    }
}