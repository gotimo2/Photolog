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


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "I CANNOT GET IT TO SHUT UP")]
        public async static Task scheduleNotification(DateTime scheduledTime, bool isOngoing)
        {

            lastRequest = new NotificationRequest
            {
                Schedule =
                {
                    NotifyTime = scheduledTime,
                    Android =
                    {
                        AllowedDelay = TimeSpan.FromMinutes(20)
                    }
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

        public static TimeSpan TimeUntilNotification()
        {
            var selectedPhotoResetTime = TimeOnly.Parse(Preferences.Default.Get<string>(PreferencesHelper.REMINDER_TIME, "00:00:00"));
            return selectedPhotoResetTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }

    }
}
