using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to} Subject: {subject} Body:{body}");
        }
    }
}
