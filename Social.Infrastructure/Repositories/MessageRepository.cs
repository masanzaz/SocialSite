using Microsoft.EntityFrameworkCore;
using Social.Application.Features.Messages;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Domain.Enums;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> _message;
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        public MessageRepository(ApplicationDbContext dbContext, IAccountService accountService) : base(dbContext)
        {
            _context = dbContext;
            _accountService = accountService;

            _message = dbContext.Set<Message>();
        }

        public async Task<int> CountByConversationId(int conversationId)
        {
            return await _message.CountAsync(x => x.MatchId == conversationId);
        }

        public async Task<IEnumerable<LastMessagesViewModel>> GetLastMessages(int personId, int pageNumber, int pageSize)
        {
            var matchBySender = await _context.match.Where(x => x.SenderId == personId && x.IsMatch == true)
                .Where(x => _context.message.Any(m => m.MatchId == x.Id))
                                .Select(x =>
                 new LastMessagesViewModel
                 {
                     Id = x.Id,
                     FirstName = x.Receiver.FirstName,
                     Image = x.Receiver.Image,
                     Content = x.Messages.OrderByDescending(x => x.Id).FirstOrDefault().Content,
                     Time = _accountService.GetTime(x.Messages.OrderByDescending(x => x.Id).FirstOrDefault().CreatedAt),
                     UnreadCount = x.Messages.Count(x => x.Status == MessageStatus.Sent && x.SenderId != personId)
                 })
                 .ToListAsync();

            var matchByReciver = await _context.match.Where(x => x.ReceiverId == personId && x.IsMatch == true)
                                .Where(x => _context.message.Any(m => m.MatchId == x.Id))
                .Select(x =>
                 new LastMessagesViewModel
                 {
                     Id = x.Id,
                     FirstName = x.Sender.FirstName,
                     Image = x.Sender.Image,
                     Content = x.Messages.OrderByDescending(x => x.Id).FirstOrDefault().Content,
                     Time = _accountService.GetTime(x.Messages.OrderByDescending(x => x.Id).FirstOrDefault().CreatedAt),
                     UnreadCount = x.Messages.Count(x => x.Status == MessageStatus.Sent && x.SenderId != personId)
                 })
                 .ToListAsync();

            if (matchBySender == null)
                return matchByReciver;
            matchBySender.AddRange(matchByReciver);

            return matchBySender;

        }

        public async Task<IEnumerable<MessagesViewModel>> GetMessagesByConversationId(int conversationId, int personId, int pageNumber, int pageSize)
        {
            var messages = _context.message.Where(x => x.MatchId == conversationId && x.SenderId != personId && x.Status == MessageStatus.Sent).ToList();
            messages.ForEach(a => a.Status = MessageStatus.Delivered);
            _context.SaveChanges();

            return await _message.Where(x => x.MatchId == conversationId)
                 .Select(x =>
                 new MessagesViewModel
                 {
                     MatchId = x.MatchId,
                     Status = (x.SenderId == personId) ? true: x.Status == (MessageStatus.Delivered)? true: false,
                     Content = x.Content,
                     Time = _accountService.GetTime(x.CreatedAt),
                     FromOther = (x.SenderId == personId) ? false : true
                 })
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .AsNoTracking()
                  .ToListAsync();
        }
    }
}
