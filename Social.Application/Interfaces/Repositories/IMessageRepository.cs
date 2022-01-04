using Social.Application.Features.Messages;
using Social.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<MessagesViewModel>> GetMessagesByConversationId(int conversationId, int personId, int pageNumber, int pageSize);
        Task<int> CountByConversationId(int conversationId);
        Task<IEnumerable<LastMessagesViewModel>> GetLastMessages(int personId, int pageNumber, int pageSize);

    }
}
