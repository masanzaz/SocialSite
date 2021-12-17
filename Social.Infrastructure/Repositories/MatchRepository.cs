using Microsoft.EntityFrameworkCore;
using Social.Application.Features.Matches;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        private readonly DbSet<Match> _match;
        public MatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _match = dbContext.Set<Match>();
        }


        public async Task<Match> GetBySenderAsync(int senderId, int receiverId)
        {
            return await _match.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);
        }

        public async Task<IEnumerable<MatchViewModel>> GetMatchesByPersonId(int personId, int pageNumber, int pageSize)
        {
            var matchBySender = await _match.Where(x => x.SenderId == personId && x.IsMatch == true)
                            .Select(x =>
                              new MatchViewModel
                              {
                                  Id = x.Id,
                                  PersonId = x.ReceiverId,
                                  PersonName = x.Receiver.FirstName,
                                  PersonImage = x.Receiver.Image
                              }).ToListAsync();

            var matchByReciver = await _match.Where(x => x.ReceiverId == personId && x.IsMatch == true)
                .Select(x =>
                  new MatchViewModel
                  {
                      Id = x.Id,
                      PersonId = x.SenderId,
                      PersonName = x.Sender.FirstName,
                      PersonImage = x.Sender.Image
                  }).ToListAsync();

            if (matchBySender == null)
                return matchByReciver;
            matchBySender.AddRange(matchByReciver);

            return matchBySender;
        }

        public async Task<int> CountByPersonId(int personId)
        {
            var matchBySender = await _match.CountAsync(x => x.SenderId == personId && x.IsMatch == true);
            var matchByReciver = await _match.CountAsync(x => x.ReceiverId == personId && x.IsMatch == true);
            return matchBySender + matchByReciver;
        }

    }
}
