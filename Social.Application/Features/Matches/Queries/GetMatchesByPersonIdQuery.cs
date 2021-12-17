using MediatR;
using Social.Application.Features.Messages;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Matches.Queries
{
    public class GetMatchesByPersonIdQuery : IRequest<PagedResponse<IEnumerable<MatchViewModel>>>
    {
        public GetMessagesParameter parameter;
    }
    public class GetMatchesByPersonIdQueryHandler : IRequestHandler<GetMatchesByPersonIdQuery, PagedResponse<IEnumerable<MatchViewModel>>>
    {
        private readonly IMatchRepository _matchRepository;

        public GetMatchesByPersonIdQueryHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<PagedResponse<IEnumerable<MatchViewModel>>> Handle(GetMatchesByPersonIdQuery request, CancellationToken cancellationToken)
        {
            var matches = await _matchRepository.GetMatchesByPersonId(request.parameter.MatchId, request.parameter.PageNumber, request.parameter.PageSize);
            var totalRecords = await _matchRepository.CountByPersonId(request.parameter.MatchId);

            return new PagedResponse<IEnumerable<MatchViewModel>>(matches, request.parameter.PageNumber, request.parameter.PageSize, totalRecords);
        }
    }
}
