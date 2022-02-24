using Social.Application.DTOs.Account;
using Social.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber, string token);
        int? GetAge(DateTime dateOfBirth);
        string GetTime(DateTime start);
        string GetLocalPath(string file, string name);
        string GenerateRamdomOtp();
    }
}
