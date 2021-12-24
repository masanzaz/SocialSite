using Social.Application.Features.Matches;
using Social.Application.Features.Persons;
using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> AddPersonHobbies(Person person, IList<int> hobbies);

        Task<Person> AddPersonDisabilities(Person person, IList<int> disabilities);

        Task<Person> GetPersonByEmail(string email);

        Task<Person> GetPersonByPhoneNumber(string phoneNumber);

        Task<PersonViewModel> GetPersonByIdWithDetails(int personId);

        Task<IEnumerable<PersonViewModel>> GetNoMatchesPerson(int personId, int pageNumber, int pageSize);
    }
}
