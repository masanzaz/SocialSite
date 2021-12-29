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
        //  Task<Response<string>> ConfirmPhoneNumber(string phoneNumber, string code);
        Task<Response<bool>> ConfirmPhoneNumber(string phoneNumber, string code);
        int? GetAge(DateTime dateOfBirth);
    }
}
