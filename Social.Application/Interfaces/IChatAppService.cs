using Social.Domain.Entities;
using System.Threading.Tasks;

namespace Social.Application.Interfaces
{
    public interface IChatAppService
    {
        Task SendMessage(Message message, string userName);

    }
}
