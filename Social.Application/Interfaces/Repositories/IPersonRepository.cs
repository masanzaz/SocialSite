using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> AddPersonHobbies(Person person, IList<int> hobbies);

        Task<Person> GetPersonByEmail(string email);

        Task<Person> GetPersonByPhoneNumber(string phoneNumber);
    }
}
