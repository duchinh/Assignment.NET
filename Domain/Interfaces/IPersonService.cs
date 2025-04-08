using Assignment2.Domain.Entities;

namespace Assignment2.Domain.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPersonById(int id);
        Task AddPerson(Person person);
        Task UpdatePerson(int id, Person person);
        Task DeletePerson(int id);
        Task<IEnumerable<Person>> FilterByName(string name);
        Task<IEnumerable<Person>> FilterByGender(string gender);
        Task<IEnumerable<Person>> FilterByBirthPlace(string birthPlace);
    }
}