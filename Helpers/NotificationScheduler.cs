using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Helpers
{
    public static class NotificationScheduler
    {
        public const string CHANNEL_NAME = "photolog_general";

        private static NotificationRequest lastRequest;


        public async static Task scheduleNotification(DateTime scheduledTime, bool isOngoing)
        {

            lastRequest = new NotificationRequest
            {
                Schedule =
                {
                    NotifyTime = scheduledTime
                },
                NotificationId = 13902,
                Title = "New photo ready!",
                Subtitle = "go to photolog to take a photo",
                Android =
                {
                    ChannelId = CHANNEL_NAME,
                    Ongoing = isOngoing
                }

            };

            await LocalNotificationCenter.Current.Show(lastRequest);
        }

        public static void closeNotification()
        {
            if (lastRequest != null)
            {
                lastRequest.Cancel();
            }
        }

    }
}
