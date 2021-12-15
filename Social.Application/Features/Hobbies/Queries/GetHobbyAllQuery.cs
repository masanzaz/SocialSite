using AutoMapper;
using MediatR;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Hobbies.Queries
{
  public class GetHobbyAllQuery : IRequest<Response<IEnumerable<HobbyViewModel>>>
    {
    }
    public class GetHobbyAllQueryHandler : IRequestHandler<GetHobbyAllQuery, Response<IEnumerable<HobbyViewModel>>>
    {
        private readonly IHobbyRepository  _hobbyRepository;
        private readonly IMapper _mapper;

        public GetHobbyAllQueryHandler(IHobbyRepository hobbyRepository, IMapper mapper)
        {
            _hobbyRepository = hobbyRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<HobbyViewModel>>> Handle(GetHobbyAllQuery request, CancellationToken cancellationToken)
        {
            var hobbies = await _hobbyRepository.GetAllAsync();
            var hobbiesViewModel = _mapper.Map<IEnumerable<HobbyViewModel>>(hobbies);
            return new Response<IEnumerable<HobbyViewModel>>(hobbiesViewModel);
        }
    }
}
