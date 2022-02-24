using MediatR;
using Social.Application.Features.Messages;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Social.Application.Features.Matches.Queries
{
 public class GetMatchesByIdQuery : IRequest<Response<MatchViewModel>>
    {
        public GetMatchParameter parameter;
    }
    public class GetMatchesByIdQueryHandler : IRequestHandler<GetMatchesByIdQuery, Response<MatchViewModel>>
    {
        private readonly IMatchRepository _matchRepository;

        public GetMatchesByIdQueryHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Response<MatchViewModel>> Handle(GetMatchesByIdQuery request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetMatchById(request.parameter.MatchId, request.parameter.PersonId);
            var totalRecords = await _matchRepository.CountByPersonId(request.parameter.MatchId);
            return new Response<MatchViewModel>(match);
        }
    }
}
