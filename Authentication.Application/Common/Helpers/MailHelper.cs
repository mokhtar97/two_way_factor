using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Helpers
{
    public static class MailHelper
    {
        private static IOptions<AppSettings> _appSettings;
        static MailHelper()
        {
            // this._appSettings = _appSettings;

        }
        public static async Task<bool> SendMail(string to, string body, IOptions<AppSettings> appSettings)
        {
            try
            {
                _appSettings = appSettings;
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress(_appSettings.Value.mailSettings.SenderName, _appSettings.Value.mailSettings.SenderEmail));
                mailMessage.To.Add(new MailboxAddress(_appSettings.Value.mailSettings.SenderName, to));
                mailMessage.Subject = "Confirm your Authentication Email";

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = body;
                mailMessage.Body = emailBodyBuilder.ToMessageBody();

                using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtpClient.Connect(_appSettings.Value.mailSettings.MailServer, _appSettings.Value.mailSettings.MailPort, _appSettings.Value.mailSettings.EnableSsl);
                    smtpClient.Authenticate(_appSettings.Value.mailSettings.SenderEmail, _appSettings.Value.mailSettings.Password);


                    try
                    {
                        await smtpClient.SendAsync(mailMessage);
                        smtpClient.Disconnect(true);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }


                }


            }

            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

    }

 }
