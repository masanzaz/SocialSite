using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Persons.Queries
{
    public class GetPersonsByInterestQuery : IRequest<PagedResponse<IEnumerable<PersonViewModel>>>
    {
        public GetPersonsParameter parameter;
    }
    public class GetPersonsByInterestQueryHandler : IRequestHandler<GetPersonsByInterestQuery, PagedResponse<IEnumerable<PersonViewModel>>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPersonsByInterestQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PersonViewModel>>> Handle(GetPersonsByInterestQuery request, CancellationToken cancellationToken)
        {
            var personViewModel = await _personRepository.GetNoMatchesPerson(request.parameter.PersonId, request.parameter.PageNumber, request.parameter.PageSize);
            var totalRecords = await _personRepository.GetNoMatchesPerson(request.parameter.PersonId);

            return new PagedResponse<IEnumerable<PersonViewModel>>(personViewModel, request.parameter.PageNumber, request.parameter.PageSize, totalRecords);
        }
    }
}
