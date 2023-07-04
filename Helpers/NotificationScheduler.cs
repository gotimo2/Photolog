using Plugin.LocalNotification;

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
            return PreferencesHelper.ReminderTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }

        public static async Task ReSchedule()
        {
            LocalNotificationCenter.Current.CancelAll();
            if (PreferencesHelper.ReminderEnabled)
            {
                var timeUntilNotificationSent = TimeUntilNotification();
                var timeNotificationWouldGoOut = DateTime.Now.Add(timeUntilNotificationSent);
                var timeUntilPhotoReady = DailyPhotoHelper.TimeUntilPhoto();


                if (timeUntilNotificationSent < timeUntilPhotoReady)
                {
                    timeNotificationWouldGoOut = timeNotificationWouldGoOut.AddDays(1);
                }
                await ScheduleNotification(timeNotificationWouldGoOut, PreferencesHelper.EnableOngoingReminder);
            }
        }

    }
}
