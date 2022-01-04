using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces;
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
    public class ConfirmPhoneNumberCommand : IRequest<Response<bool>>
    {
        public string phoneNumber { get; set; }
        public string code { get; set; }

    }
    public class ConfirmPhoneNumberCommandHandler : IRequestHandler<ConfirmPhoneNumberCommand, Response<bool>>
    {
        private readonly IAccountService _accountService;
        public ConfirmPhoneNumberCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<bool>> Handle(ConfirmPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var response = await _accountService.ConfirmPhoneNumber(request.phoneNumber, request.code);


            // return new Response<int>(true);
            return new Response<bool>(true); ;
        }
    }
}
