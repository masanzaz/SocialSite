using Microsoft.Extensions.Logging;
using Social.Application.Exceptions;
using Social.Application.Interfaces;
using System.Threading.Tasks;

namespace Social.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public ILogger<EmailService> _logger { get; }

        public SmsService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public async Task SendSmsAsync(string number, string message)
        {
            try
            {
                // message
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }

        }
    }
}
