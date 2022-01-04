using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using Social.Domain.Entities.Auth;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Persons.Commands
{
    public class CreatePersonCommand : IRequest<Response<int>>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int GenreId { get; set; }
        public int InterestedId { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int[] hobbies { get; set; }
        public int[] disabilities { get; set; }

    }
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Response<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public CreatePersonCommandHandler(IPersonRepository personRepository,
            IUserRepository userRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<Person>(request);
            if (_userRepository.IsUniqueEmail(request.Email))
                throw new BadRequestException($"Email - {request.Email} already exist");
            if (_userRepository.IsUniquePhoneNumber(request.PhoneNumber))
                throw new BadRequestException($"Phone Number - {request.PhoneNumber} already exist");
            if (!_genreRepository.GenreExist(person.GenreId))
                throw new NotFoundException($"Genre does not exist");

            User user = new User() { Email = request.Email, PhoneNumber = request.PhoneNumber };
            user = await _userRepository.AddPersonRol(user);
            person.User = user;
            person = await _personRepository.AddPersonHobbies(person, request.hobbies);
            person = await _personRepository.AddPersonDisabilities(person, request.disabilities);
            person.Image = (person.GenreId == 1) ? "images/man_no_image.png" : "images/woman_no_image.png";
            await _personRepository.AddAsync(person);

            return new Response<int>(person.Id);
        }
    }
}
