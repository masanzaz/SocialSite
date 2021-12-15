using Microsoft.EntityFrameworkCore;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly DbSet<Person> _person;
        private readonly ApplicationDbContext _context;
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _person = dbContext.Set<Person>();
        }

        public async Task<Person> AddPersonHobbies(Person person, IList<int> hobbies)
        {
            person.Hobbies = new List<PersonHobby>();
            foreach (var hobbyId in hobbies)
            {
                var hobby = await _context.hobby.FirstOrDefaultAsync(x => x.Id == hobbyId);
                if (hobby == null)
                    throw new NotFoundException($"Hobby - {hobbyId} is not found");
                person.Hobbies.Add(new PersonHobby { Person = person, Hobby = hobby });
            }
            return person;
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;
            return await _person.FirstOrDefaultAsync(x => x.UserId == user.Id);
        }

        public async Task<Person> GetPersonByPhoneNumber(string phoneNumber)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                return null;
            return await _person.FirstOrDefaultAsync(x => x.UserId == user.Id);
        }
    }
}