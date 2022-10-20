using Twilio.Rest.Api.V2010.Account;

namespace Authentication.Infrastructure.NotificationServices.SMSService.Helpers
{
    public interface ISmsTwilio
    {
        MessageResource SendSMS(string body, string from, string to);
    }
}