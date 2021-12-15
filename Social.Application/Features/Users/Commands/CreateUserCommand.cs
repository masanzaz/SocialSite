using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities.Auth;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int[] roles { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            if (_userRepository.IsUniqueEmail(request.Email))
                throw new BadRequestException($"Email - {request.Email} already exist");

            if (_userRepository.IsUniquePhoneNumber(request.PhoneNumber))
                throw new BadRequestException($"Phone Number - {request.PhoneNumber} already exist");

            user = await _userRepository.AddUserRoles(user, request.roles);
            await _userRepository.AddAsync(user);
            return new Response<int>(user.Id);
        }
    }
}
