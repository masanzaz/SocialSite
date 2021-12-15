using Social.Application.Interfaces;
using Social.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public async Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber, string code)
        {
            //validate code
            return new Response<bool>(true);

            //return new Response<string>(phoneNumber, message: $"Phone Number {phoneNumber} Confirmed ");
        }
    }
}
