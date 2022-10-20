using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Helpers
{
    public static class SendNotificationFCM
    {

        public async static void SendSubscriptionFCM(string title,string body,string userId)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=AAAAukomnk0:APA91bEkHzgShI_eshHXWgu-Dzozb3obmds7pyDeaG55mAIdGnSJCuPJGDcOIVgzinRR_EPRuWzQ1H4ziXlIxby9plOpfnkMYNX8vja40GIweRxRmsyMoAz2qhl_S1Lh0MupExoqtbxR");
            const string url = "https://fcm.googleapis.com/fcm/send";

            var notificationDetails = new NotificationDetailsDto()
            {
                content_available = true,
                priority = "high",
                notificationId = 2,
                sound = "default",
                title = title,
                body = body
            };

            // Notification
            var requestData = new NotificationDto()
              {
              notification = notificationDetails, // IOS
                data = notificationDetails, // Android
                to = $"/topics/{userId}"
             };


            // Send Notification
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, System.Text.Encoding.Default, "application/json");
            var result = await httpClient.PostAsync(url, content);
        }
    }

    public class NotificationDetailsDto
    {
        public string body { get; set; }
        public string priority { get; set; }
        public string title { get; set; }
        public string sound { get; set; }
        public int notificationId { get; set; }
        public bool content_available { get; set; }
    }

    public class NotificationDto
    {
        public string to { get; set; } 
        public NotificationDetailsDto notification { get; set; }
        public NotificationDetailsDto data { get; set; }
    }
}
