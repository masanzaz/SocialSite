using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
