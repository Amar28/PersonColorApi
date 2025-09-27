using PersonColorApi.Domain.Entities;

namespace PersonColorApi.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetByColorAsync(string color);
    }
}
