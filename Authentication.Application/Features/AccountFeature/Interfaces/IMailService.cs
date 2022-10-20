using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.AccountFeature.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMailResetPasswordAsync(string to, string userName, string token);
        public Task<bool> SendConfirmationMailAsync(string to, string confirmation);
        Task<bool> SendMailForgetPasswordAsync(string to, string userName, string token);
    }
}
