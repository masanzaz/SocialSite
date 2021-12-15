using Social.Domain.Entities.Auth;
using System.Threading.Tasks;

namespace Social.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> AddUserRoles(User user, int[] roles);

        Task<User> AddPersonRol(User user);

        bool IsUniquePhoneNumber(string phoneNumber);

        bool IsUniqueEmail(string email);
    }
}
