using Microsoft.EntityFrameworkCore;
using Social.Application.Exceptions;
using Social.Application.Features.Hobbies;
using Social.Application.Features.Matches;
using Social.Application.Features.Persons;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<PersonViewModel>> GetNoMatchesPerson(int personId, int pageNumber, int pageSize)
        {
            var person = await _person.FirstOrDefaultAsync(x => x.Id == personId);
            if(person == null)
                throw new NotFoundException($"Person - {personId} is not found");

            var personww = await _person.Where(x => x.Id != personId).Select(x =>
             new PersonViewModel
             {
                 FirstName = x.FirstName,
                 LasName = x.LasName,
                 Image = x.Image,
                 GenreId = x.GenreId,
                 GenreName = x.Genre.Name
             }).FirstOrDefaultAsync();

            return null;
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;
            return await _person.FirstOrDefaultAsync(x => x.UserId == user.Id);
        }

        public async Task<PersonViewModel> GetPersonByIdWithDetails(int personId)
        {
            return await _person.Where(x => x.Id == personId).Select(x =>
                          new PersonViewModel
                          {
                              FirstName = x.FirstName,
                              LasName = x.LasName,
                              Image = x.Image,
                              GenreId = x.GenreId,
                              InterestedId = x.InterestedId,
                              GenreName = x.Genre.Name,
                              hobbies = x.Hobbies.Select(s => new HobbyViewModel { Id = s.HobbyId, Image = s.Hobby.Image, Name = s.Hobby.Name }).ToList()
                          }).FirstOrDefaultAsync();
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