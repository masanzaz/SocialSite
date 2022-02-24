using Microsoft.EntityFrameworkCore;
using Social.Application.Features.Matches;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        private readonly DbSet<Match> _match;
        private readonly IAccountService _accountService;
        public MatchRepository(ApplicationDbContext dbContext, IAccountService accountService) : base(dbContext)
        {
            _match = dbContext.Set<Match>();
            _accountService = accountService;
        }


        public async Task<Match> GetBySenderAsync(int senderId, int receiverId)
        {
            return await _match.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);
        }

        public async Task<IEnumerable<MatchViewModel>>
            GetMatchesByPersonId(int personId, int pageNumber, int pageSize)
        {


            var matchBySender = await _match.Where(x => x.SenderId == personId && x.IsMatch == true)
                            .Select(x =>
                              new MatchViewModel
                              {
                                  MatchId = x.Id,
                                  id = x.ReceiverId,
                                  FirstName = x.Receiver.FirstName,
                                  LasName = x.Receiver.LasName,
                                  Image = x.Receiver.Image,
                                  MatchImage = x.Sender.Image,
                                  City = x.Receiver.City,
                                  About = x.Receiver.About,
                                  Age = _accountService.GetAge(x.Receiver.DateOfBirth ?? DateTime.MinValue)
                              }).ToListAsync();

            var matchByReciver = await _match.Where(x => x.ReceiverId == personId && x.IsMatch == true)
                .Select(x =>
                  new MatchViewModel
                  {
                      MatchId = x.Id,
                      id = x.SenderId,
                      FirstName = x.Sender.FirstName,
                      LasName = x.Sender.LasName,
                      Image = x.Sender.Image,
                      MatchImage = x.Receiver.Image,
                      City = x.Sender.City,
                      About = x.Sender.About,
                      Age = _accountService.GetAge(x.Sender.DateOfBirth ?? DateTime.MinValue)
                  }).ToListAsync();

            if (matchBySender == null)
                return matchByReciver;
            matchBySender.AddRange(matchByReciver);

            return matchBySender;
        }

        public async Task<MatchViewModel> GetMatchById(int matchId, int personId)
        {
            var item = await _match.Include(x => x.Sender).Include(x => x.Receiver)
                .FirstOrDefaultAsync(x => x.Id == matchId);
            var match =
                            new MatchViewModel
                            {
                                MatchId = item.Id,
                                id = (item.SenderId == personId) ? item.ReceiverId : item.SenderId,
                                FirstName = (item.SenderId == personId) ? item.Receiver.FirstName : item.Sender.FirstName,
                                LasName = (item.SenderId == personId) ? item.Receiver.LasName : item.Sender.LasName,
                                Image = (item.SenderId == personId) ? item.Receiver.Image : item.Sender.Image,
                                MatchImage = (item.SenderId != personId) ? item.Receiver.Image : item.Sender.Image,
                                City = (item.SenderId == personId) ? item.Receiver.City : item.Sender.City,
                                About = (item.SenderId == personId) ? item.Receiver.About : item.Sender.About,
                                Age = _accountService.GetAge((item.SenderId == personId) ? item.Receiver.DateOfBirth ?? DateTime.MinValue : item.Sender.DateOfBirth ?? DateTime.MinValue)
                            };

            return match;
        }


        public async Task<int> CountByPersonId(int personId)
        {
            var matchBySender = await _match.CountAsync(x => x.SenderId == personId && x.IsMatch == true);
            var matchByReciver = await _match.CountAsync(x => x.ReceiverId == personId && x.IsMatch == true);
            return matchBySender + matchByReciver;
        }

    }
}
