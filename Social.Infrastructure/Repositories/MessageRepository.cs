using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> _message;
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _message = dbContext.Set<Message>();
        }

        public async Task<int> CountByConversationId(int conversationId)
        {
            return await _message.CountAsync(x => x.MatchId == conversationId);
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationId(int conversationId, int pageNumber, int pageSize)
        {
          return  await _message.Where(x=> x.MatchId == conversationId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
