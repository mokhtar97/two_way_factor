using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Helpers
{
    public class AppSettings
    {
        public string UserDefaultPassword { get; set; }
        public string PasswordTokenValidity { get; set; }
        //    public EmailSettings EmailSettings { get; set; }
        public string FrontendUrl { get; set; }
        public EmailSettings mailSettings { get; set; }
        public SMSSettings SMSSettings { get; set; }

    }
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailKey { get; set; }
    }

    public class SMSSettings
    {
        public string Username { get; set; }
        public string SystemID { get; set; }
        public string Alias { get; set; }
        public string API_KEY { get; set; }
        public string JWTToken { get; set; }
        public string Interface_Version { get; set; }
        public string System_Type { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Throughput { get; set; }
        public string _of_Sessions { get; set; }
    }
}
