using AutoMapper;
using MediatR;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Application.Features.Matches.Commands
{
  public class CancelMatchCommand : IRequest<Response<int>>
    {
        public int MatchId { get; set; }
    }
    public class CancelMatchCommandCommandHandler : IRequestHandler<CancelMatchCommand, Response<int>>
    {
        private readonly IMatchRepository _matchRepository;
        public CancelMatchCommandCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Response<int>> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.MatchId);
            if (match == null)
                throw new NotFoundException($"Match - {request.MatchId} not found!!");

            await _matchRepository.DeleteAsync(match);

            return new Response<int>(match.Id);
        }
    }
}
