using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<Person> AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetByNameAsync(string name)
        {
            return await _context.People
                .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByGenderAsync(string gender)
        {
            if (Enum.TryParse<Gender>(gender, true, out var genderEnum))
            {
                return await _context.People
                    .Where(p => p.Gender == genderEnum)
                    .ToListAsync();
            }
            return Enumerable.Empty<Person>();
        }

        public async Task<IEnumerable<Person>> GetByBirthPlaceAsync(string birthPlace)
        {
            return await _context.People
                .Where(p => p.BirthPlace.Contains(birthPlace))
                .ToListAsync();
        }
    }
}
