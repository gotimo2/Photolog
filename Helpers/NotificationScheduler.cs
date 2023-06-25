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

        public async static Task ScheduleNotification(DateTime scheduledTime, bool isOngoing)
        {

            var request = new NotificationRequest
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

            await LocalNotificationCenter.Current.Show(request);
        }

        public static void CancelNotification()
        {
            LocalNotificationCenter.Current.CancelAll();
        }

        public static TimeSpan TimeUntilNotification()
        {
            var selectedPhotoResetTime = TimeOnly.Parse(Preferences.Default.Get<string>(PreferencesHelper.REMINDER_TIME, "00:00:00"));
            return selectedPhotoResetTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }

        public static async Task Schedule()
        {
            await ScheduleNotification(DateTime.Now.Add(TimeUntilNotification()), Preferences.Default.Get<bool>(PreferencesHelper.ONGOING_REMINDER, false));
        }
        

    }
}
