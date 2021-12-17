using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByConversationId(int conversationId, int pageNumber, int pageSize);
        Task<int> CountByConversationId(int conversationId);
    }
}
