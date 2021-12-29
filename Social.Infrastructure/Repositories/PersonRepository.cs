using Microsoft.EntityFrameworkCore;
using Social.Application.Exceptions;
using Social.Application.Features.Disabilities;
using Social.Application.Features.Hobbies;
using Social.Application.Features.Matches;
using Social.Application.Features.Persons;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;


        private readonly IAccountService _accountService;
        public PersonRepository(ApplicationDbContext dbContext, IAccountService accountService) : base(dbContext)
        {
            _context = dbContext;
            _accountService = accountService;

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

        public async Task<Person> AddPersonDisabilities(Person person, IList<int> disabilities)
        {
            person.Disabilities = new List<PersonDisability>();
            foreach (var disabilityId in disabilities)
            {
                var disability = await _context.disability.FirstOrDefaultAsync(x => x.Id == disabilityId);
                if (disability == null)
                    throw new NotFoundException($"Disability - {disabilityId} is not found");
                person.Disabilities.Add(new PersonDisability { Person = person, Disability = disability });
            }
            return person;
        }

        public async Task<IEnumerable<PersonViewModel>> GetNoMatchesPerson(int personId, int pageNumber, int pageSize)
        {
            var person = await _context.person.FirstOrDefaultAsync(x => x.Id == personId);
            if (person == null)
                throw new NotFoundException($"Person - {personId} is not found");

            //  var personView = await _person.Where(x => x.Id != personId && x.GenreId == person.InterestedId)
            var personView = await _context.person.Where(x => x.Id != personId)
                .Select(x =>
                 new PersonViewModel
                 {
                     id = x.Id,
                     FirstName = x.FirstName,
                     LasName = x.LasName,
                     Image = x.Image,
                     City = x.City,
                     About = x.About,
                     Age = _accountService.GetAge(x.DateOfBirth ?? DateTime.MinValue),
                     GenreId = x.GenreId,
                     GenreName = x.Genre.Name
                 }).ToListAsync();

            return personView;
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;
            return await _context.person.FirstOrDefaultAsync(x => x.UserId == user.Id);
        }

        public async Task<PersonViewModel> GetPersonByIdWithDetails(int personId)
        {
            return await _context.person.Where(x => x.Id == personId).Select(x =>
                          new PersonViewModel
                          {
                              id = x.Id,
                              FirstName = x.FirstName,
                              LasName = x.LasName,
                              Image = x.Image,
                              GenreId = x.GenreId,
                              About = x.About,
                              City = x.City,
                              InterestedId = x.InterestedId,
                              GenreName = x.Genre.Name,
                              hobbies = x.Hobbies.Select(s => s.HobbyId).ToList(),
                              disabilities = x.Disabilities.Select(s => s.DisabilityId).ToList()
                          }).FirstOrDefaultAsync();
        }

        public async Task<PersonViewModel> GetPersonByPhoneWithDetails(string phoneNumber)
        {

            var user = _context.user.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                throw new NotFoundException("The phone number does not exist");
            return await _context.person.Where(x => x.UserId == user.Id).Select(x =>
              new PersonViewModel
              {
                  id = x.Id,
                  FirstName = x.FirstName,
                  LasName = x.LasName,
                  Image = x.Image,
                  GenreId = x.GenreId,
                  About = x.About,
                  City = x.City,
                  InterestedId = x.InterestedId,
                  GenreName = x.Genre.Name,
                  hobbies = x.Hobbies.Select(s => s.HobbyId).ToList(),
                  disabilities = x.Disabilities.Select(s => s.DisabilityId).ToList()
              }).FirstOrDefaultAsync();
        }

        public async Task<Person> GetPersonByPhoneNumber(string phoneNumber)
        {
            var user = await _context.user.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                return null;
            return await _context.person.FirstOrDefaultAsync(x => x.UserId == user.Id);
        }

        public async Task<int> GetNoMatchesPerson(int personId)
        {
            return await _context.person.CountAsync(x => x.Id != personId && x.GenreId == personId);
        }
    }
}