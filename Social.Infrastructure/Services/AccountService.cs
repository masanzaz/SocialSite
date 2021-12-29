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

        public int? GetAge(DateTime dateOfBirth)
        {
            if (dateOfBirth == DateTime.MinValue)
                return null;
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }
    }
}
