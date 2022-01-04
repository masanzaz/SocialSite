using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using Social.Domain.Entities.Auth;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Persons.Commands
{
    public class UpdatePersonCommand : IRequest<Response<int>>
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public string DateOfBirth { get; set; }

    }
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Response<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAccountService _accountService;
        public UpdatePersonCommandHandler(IPersonRepository personRepository,
            IAccountService accountService)
        {
            _personRepository = personRepository;
            _accountService = accountService;
        }

        public async Task<Response<int>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.id);
            if (person == null)
                throw new NotFoundException($"Person does not exist");
            person.FirstName = String.IsNullOrWhiteSpace(request.FirstName) ? person.FirstName : request.FirstName;
            person.About = String.IsNullOrWhiteSpace(request.About) ? person.About : request.About;
            person.LasName = String.IsNullOrWhiteSpace(request.LasName) ? person.LasName : request.LasName;
            person.Image = _accountService.GetLocalPath(request.Image, request.id.ToString());
            if (!String.IsNullOrWhiteSpace(request.DateOfBirth))
                person.DateOfBirth = DateTime.Parse(request.DateOfBirth);

            await _personRepository.UpdateAsync(person);

            return new Response<int>(person.Id);
        }
    }
}
