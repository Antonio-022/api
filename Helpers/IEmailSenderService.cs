using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SAADS_CORE_61.Helpers
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(MailRequest request);
    }
}
