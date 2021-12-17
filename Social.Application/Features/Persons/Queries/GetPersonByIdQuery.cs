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
  public class GetPersonByIdQuery : IRequest<Response<PersonViewModel>>
    {
        public int Id { get; set; }
        public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Response<PersonViewModel>>
        {
            private readonly IPersonRepository _personRepository;
            public GetPersonByIdQueryHandler(IPersonRepository personRepository)
            {
                _personRepository = personRepository;
            }
            public async Task<Response<PersonViewModel>> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
            {
                PersonViewModel person = await _personRepository.GetPersonByIdWithDetails(query.Id);
                if (person == null) throw new NotFoundException($"Person Not Found.");
               
                return new Response<PersonViewModel>(person);
            }
        }



    }
}
