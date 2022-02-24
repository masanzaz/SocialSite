using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Application.Wrappers;
using Social.Domain.Entities.Auth;
using Social.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        private readonly DbSet<UserToken> _userToken;
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _context;

        public UserTokenRepository(ApplicationDbContext dbContext, IAccountService accountService) : base(dbContext)
        {
            _accountService = accountService;
            _context = dbContext;
            _userToken = dbContext.Set<UserToken>();
        }

        public async Task<Response<bool>> ValidateTokenByPhone(string phoneNumber, string token)
        {
            var userToken = _userToken.FirstOrDefault(x => x.PhoneNumber == phoneNumber && x.Token == token);
            if (userToken == null)
                return new Response<bool>(false);
            var currentdate = DateTime.Now;
            bool response = (userToken.ValidUntil >= currentdate) ? true : false;
            return new Response<bool>(response);
        }

        public Task<Response<bool>> ValidateTokenByMail(string mail, string token)
        {
            //   return _userToken.AnyAsync(x => x.Email == mail && x.Token == token).Result;
            return null;
        }

        public Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber)
        {
            string token = _accountService.GenerateRamdomOtp();

            var list = _context.userToken.Where(x => x.PhoneNumber == phoneNumber);
            _context.RemoveRange(list);
            _context.SaveChanges();

            UserToken userToken = new UserToken();
            userToken.PhoneNumber = phoneNumber;
            userToken.Token = token;
            userToken.CreationDate = DateTime.Now;
            userToken.ValidUntil = DateTime.Now.AddMinutes(5);
            _context.userToken.Add(userToken);
            _context.SaveChanges();

            var response = _accountService.ConfirmPhoneNumber(phoneNumber, token);
            return response;
        }
    }
}
