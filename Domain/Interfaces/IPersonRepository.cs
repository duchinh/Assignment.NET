using Assignment2.Domain.Entities;

namespace Assignment2.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Person person);
        Task<IEnumerable<Person>> GetByNameAsync(string name);
        Task<IEnumerable<Person>> GetByGenderAsync(string gender);
        Task<IEnumerable<Person>> GetByBirthPlaceAsync(string birthPlace);
    }
}

