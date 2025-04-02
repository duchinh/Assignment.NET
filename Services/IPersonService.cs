using Bai2.Models;

namespace Bai2.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person? GetById(int id);
        void Create(Person person);
        void Update(Person person);
        void Delete(int id);
        IEnumerable<Person> GetMaleMembers();
        Person GetOldestMember();
        string GetFullName(int id);
        IEnumerable<Person> GetMembersByBirthYear(int year);
        IEnumerable<Person> GetMembersBornAfter2000();
        IEnumerable<Person> GetMembersBornBefore1990();
        IEnumerable<Person> GetGraduatedMembers();
        IEnumerable<Person> GetMembersByBirthPlace(string birthPlace);
        PaginationModel<Person> GetPagination(int page, int pageSize);
        string GetMaleMembersString();
        string GetOldestMemberString();
        string GetFullNamesString();
        string GetMembersByBirthYearString(string filter);
        string GetExportExcelString();
        Person GetPersonDetails(int id);
        Person GetPersonForEdit(int id);
        Person GetPersonForDelete(int id);
        object CreatePerson(Person person);
        object UpdatePerson(Person person);
        object DeletePerson(int id);
    }
}