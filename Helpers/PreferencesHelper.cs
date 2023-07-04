namespace Photolog.Helpers
{
    public static class PreferencesHelper
    {
        public const string LAST_PHOTO_TIME = "last_photo_time";
        public const string REMINDER_TIME = "reminder_time";
        public const string RESET_TIME = "reset_time";

        public const string HAS_HAD_WELCOME = "has_had_welcome";
        public const string REMINDER_ENABLED = "reminder_enabled";
        public const string ONGOING_REMINDER = "ongoing_reminder";

        public static DateTime LastPhotoTime => Preferences.Default.Get<DateTime>(LAST_PHOTO_TIME, DateTime.UnixEpoch);
        public static TimeOnly ReminderTime => TimeOnly.Parse(Preferences.Default.Get<string>(REMINDER_TIME, "00:00:00"));
        public static TimeOnly ResetTime => TimeOnly.Parse(Preferences.Default.Get<string>(RESET_TIME, "00:00:00"));
        public static bool EnableOngoingReminder => Preferences.Default.Get<bool>(ONGOING_REMINDER, false);
        public static bool ReminderEnabled => Preferences.Default.Get<bool>(REMINDER_ENABLED, false);
        public static bool HasHadWelcome => Preferences.Default.Get<bool>(HAS_HAD_WELCOME, false);

        public static void SetDefaultPreferences()
        {
            Preferences.Default.Clear();
            Preferences.Default.Set(LAST_PHOTO_TIME, DateTime.UnixEpoch);
            Preferences.Default.Set(RESET_TIME, "00:00:00");
            Preferences.Default.Set(HAS_HAD_WELCOME, true);
            Preferences.Default.Set(REMINDER_TIME, "12:00:00");
            Preferences.Default.Set(REMINDER_ENABLED, false);
            Preferences.Default.Set(ONGOING_REMINDER, false);
        }

    }
}
