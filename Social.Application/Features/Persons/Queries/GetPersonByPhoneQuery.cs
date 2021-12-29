using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Persons.Queries
{
  public class GetPersonByPhoneQuery : IRequest<Response<PersonViewModel>>
    {
        public string phoneNumber { get; set; }
        public class GetPersonByPhoneQueryHandler : IRequestHandler<GetPersonByPhoneQuery, Response<PersonViewModel>>
        {
            private readonly IPersonRepository _personRepository;
            public GetPersonByPhoneQueryHandler(IPersonRepository personRepository)
            {
                _personRepository = personRepository;
            }
            public async Task<Response<PersonViewModel>> Handle(GetPersonByPhoneQuery query, CancellationToken cancellationToken)
            {
                PersonViewModel person = await _personRepository.GetPersonByPhoneWithDetails(query.phoneNumber);
                if (person == null) throw new NotFoundException($"Person Not Found.");

                return new Response<PersonViewModel>(person);
            }
        }



    }
}
