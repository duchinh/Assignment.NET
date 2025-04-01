using Assignment1.Models;

namespace Assignment1.Services
{
    public interface IPersonService
    {
        List<Person> GetAll();
        List<Person> GetMaleMembers();
        Person GetOldestMember();
        List<string> GetFullNames();
        List<Person> GetMembersByBirthYear(int year);
        List<Person> GetMembersBornAfterYear(int year);
        List<Person> GetMembersBornBeforeYear(int year);

    }
}