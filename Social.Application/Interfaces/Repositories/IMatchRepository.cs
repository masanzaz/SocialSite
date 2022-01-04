using Social.Application.Features.Matches;
using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<Match> GetBySenderAsync(int senderId, int reciverId);
        Task<IEnumerable<MatchViewModel>> GetMatchesByPersonId(int personId, int pageNumber, int pageSize);
        Task<int> CountByPersonId(int personId);

    }
}
