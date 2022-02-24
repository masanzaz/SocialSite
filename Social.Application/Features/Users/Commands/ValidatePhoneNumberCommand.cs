using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Social.Application.Features.Users.Commands
{
    public class ValidatePhoneNumberCommand : IRequest<Response<bool>>
    {
        public string phoneNumber { get; set; }
        public string token { get; set; }
    }
    public class ValidatePhoneNumberCommandHandler : IRequestHandler<ValidatePhoneNumberCommand, Response<bool>>
    {
        private readonly IUserTokenRepository _personRepository;
        public ValidatePhoneNumberCommandHandler(IUserTokenRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Response<bool>> Handle(ValidatePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var response = await _personRepository.ValidateTokenByPhone(request.phoneNumber, request.token);
            return response;
        }
    }
}
