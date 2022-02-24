using Social.Application.Wrappers;
using Social.Domain.Entities.Auth;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
        Task<Response<bool>> ValidateTokenByPhone(string phoneNumber, string token);
        Task<Response<bool>> ValidateTokenByMail(string mail, string token);
        Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber);
    }
}
