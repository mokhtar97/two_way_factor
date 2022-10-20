using Authentication.Application.Common.Helpers;
using Authentication.Application.Features.AccountFeature.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Authentication.Application.Features.AccountFeature
{
    public class MailService : IMailService
    {
        IOptions<AppSettings> _appSettings;
        //  private MailHelper _mailHelper;
        private IHostingEnvironment _environment;

        public MailService(IOptions<AppSettings> appSettings, IHostingEnvironment environment)
        {
            this._appSettings = appSettings;
            this._environment = environment;
            //  this._mailHelper = new MailHelper(_appSettings);
        }

        public async Task<bool> SendMailResetPasswordAsync(string to, string userName, string token)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Template\\ResetPasswordTemplate.html");
            string body = string.Empty;
            body = File.ReadAllText(path);
            var urlRestPassword = _appSettings.Value.FrontendUrl + "/reset-password?token=" + token;
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Url}", urlRestPassword);

            return await MailHelper.SendMail(to, body, _appSettings);
        }

        public async Task<bool> SendMailForgetPasswordAsync(string to, string userName, string code)
        {
           
            string body = string.Empty;
            body = "Hello,"+ userName + " from Authentication this is Your Verification Code "+ code;

            return await MailHelper.SendMail(to, body, _appSettings);
        }

        public async Task<bool> SendConfirmationMailAsync(string to, string confirmation)
        {
            return await MailHelper.SendMail(to, confirmation, _appSettings);
        }
    }
}
