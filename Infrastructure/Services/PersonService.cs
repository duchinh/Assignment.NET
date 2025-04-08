using Assignment2.Domain.Interfaces;
using Assignment2.Domain.Entities;

namespace Assignment2.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException($"Person with ID {id} not found.");
            return person;
        }

        public async Task AddPerson(Person person)
        {
            await _personRepository.AddAsync(person);
        }

        public async Task UpdatePerson(int id, Person person)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id);
            if (existingPerson == null)
                throw new KeyNotFoundException($"Person with ID {id} not found.");

            person.Id = id;
            await _personRepository.UpdateAsync(person);
        }

        public async Task DeletePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException($"Person with ID {id} not found.");

            await _personRepository.DeleteAsync(person);
        }

        public async Task<IEnumerable<Person>> FilterByName(string name)
        {
            return await _personRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Person>> FilterByGender(string gender)
        {
            return await _personRepository.GetByGenderAsync(gender);
        }

        public async Task<IEnumerable<Person>> FilterByBirthPlace(string birthPlace)
        {
            return await _personRepository.GetByBirthPlaceAsync(birthPlace);
        }
    }
} 