
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Authentication.Infrastructure.NotificationServices.SMSService.Helpers
{
    public class SmsTwilio : ISmsTwilio
    {
        public SmsTwilio(
            ) {
            TwilioClient.Init("AC93897fd58c0c55c62705e87a51525b7f", "fdde34f2b4ca5c5a87563e4881473a4e");
        }

        public MessageResource SendSMS(string body, string from, string to)
        {
            
            var sms = MessageResource.Create(
                    body:"hello from magnificent your verification code is " + body,
                    from: new Twilio.Types.PhoneNumber(from),
                    to: new Twilio.Types.PhoneNumber(to));

            return sms;
        }

       
    }
}
