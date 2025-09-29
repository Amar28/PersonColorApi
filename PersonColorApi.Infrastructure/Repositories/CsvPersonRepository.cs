using PersonColorApi.Domain.Entities;
using PersonColorApi.Domain.Interfaces;
using System.Text;
using System.Globalization;

namespace Infrastructure.Repositories
{
    public class CsvPersonRepository : IPersonRepository
    {
        private readonly string _csvFilePath;
        private readonly List<Person> _inMemoryPersons = new();
        private readonly Dictionary<int, string> _colorMap = new()
        {
            {1,"blau"}, {2,"grün"}, {3,"violett"}, {4,"rot"}, {5,"gelb"}, {6,"türkis"}, {7,"weiß"}
        };

        public CsvPersonRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        private async Task<List<Person>> LoadCsvAsync()
        {
            var persons = new List<Person>();
            var lines = await File.ReadAllLinesAsync(_csvFilePath);

            int id = 1;
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 4) continue;

                string lastName = parts[0].Trim();
                string name = parts[1].Trim();
                string zipCity = parts[2].Trim();
                string colorCodeStr = parts.Length > 3 ? parts[3].Trim() : "0";

                string zip = zipCity.Split(' ')[0];
                string city = string.Join(' ', zipCity.Split(' ')[1..]);
                string color = _colorMap.ContainsKey(int.Parse(colorCodeStr)) ? _colorMap[int.Parse(colorCodeStr)] : "unknown";

                persons.Add(new Person
                {
                    Id = id++,
                    LastName = lastName,
                    Name = name,
                    ZipCode = zip,
                    City = city,
                    Color = color
                });
            }
            return persons;
        }

        private async Task SaveCsvAsync(List<Person> persons)
        {
            var lines = persons.Select(p =>
                $"{p.LastName}, {p.Name}, {p.ZipCode} {p.City}, {_colorMap.First(c => c.Value == p.Color).Key}"
            );
            await File.WriteAllLinesAsync(_csvFilePath, lines, Encoding.UTF8);
        }

        public async Task<Person> AddAsync(Person person)
        {
            var persons = await LoadCsvAsync();
            person.Id = persons.Count + 1;
            persons.Add(person);
            await SaveCsvAsync(persons);
            return person;
        }

        public async Task<IEnumerable<Person>> GetAllAsync() => await LoadCsvAsync();
        public async Task<Person?> GetByIdAsync(int id) => (await LoadCsvAsync()).FirstOrDefault(p => p.Id == id);
        public async Task<IEnumerable<Person>> GetByColorAsync(string color) => (await LoadCsvAsync()).Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
    }
}