using Microsoft.EntityFrameworkCore;
using Social.Application.Exceptions;
using Social.Application.Interfaces.Repositories;
using Social.Domain.Entities.Auth;
using Social.Domain.Enums;
using Social.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> _user;
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _user = dbContext.Set<User>();
        }

        public async Task<User> AddPersonRol(User user)
        {
            user.Roles = new List<UserRole>();
            var role = await _context.role.FirstOrDefaultAsync(x => x.Name == RolEnum.Person.ToString());
            if (role == null)
                throw new NotFoundException($"Role Person does not exist");
            user.Roles.Add(new UserRole { User = user, Role = role });
            return user;
        }

        public async Task<User> AddUserRoles(User user, int[] roles)
        {
            user.Roles = new List<UserRole>();
            foreach (var rolId in roles)
            {
                var role = await _context.role.FirstOrDefaultAsync(x => x.Id == rolId);
                if (role == null)
                {
                    throw new NotFoundException($"Role - {rolId} is not found");
                }
                user.Roles.Add(new UserRole { User = user, Role = role });
            }
            return user;
        }

        public bool IsUniqueEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return _user.AnyAsync(p => p.Email == email).Result;
        }

        public bool IsUniquePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
            return _user.AnyAsync(p => p.PhoneNumber == phoneNumber).Result;
        }
    }
}