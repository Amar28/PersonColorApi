using System.Text;
using PersonColorApi.Domain.Entities;
using Infrastructure.Repositories;

public class CsvPersonRepositoryTests
{
    private async Task<string> CreateTempCsvAsync(string content)
    {
        var tempFile = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFile, content, Encoding.UTF8);
        return tempFile;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReadPersonsFromCsv()
    {
        var csv = "Müller, Hans, 12345 Berlin, 1";
        var path = await CreateTempCsvAsync(csv);
        var repo = new CsvPersonRepository(path);

        var persons = await repo.GetAllAsync();

        Assert.Single(persons);
        Assert.Equal("Müller", persons.First().LastName);
        Assert.Equal("Hans", persons.First().Name);
        Assert.Equal("blau", persons.First().Color);
    }

    [Fact]
    public async Task AddAsync_ShouldAppendPersonToCsv()
    {
        var csv = "Müller, Hans, 12345 Berlin, 1";
        var path = await CreateTempCsvAsync(csv);
        var repo = new CsvPersonRepository(path);

        var newPerson = new Person
        {
            LastName = "Test",
            Name = "Max",
            ZipCode = "67890",
            City = "Hamburg",
            Color = "rot"
        };

        var created = await repo.AddAsync(newPerson);
        var all = await repo.GetAllAsync();

        Assert.Equal(2, all.Count());
        Assert.Equal("Max", created.Name);
        Assert.Equal("rot", created.Color);
    }

}
