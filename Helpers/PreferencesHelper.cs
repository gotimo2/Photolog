﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool PreferencesExist()

        {
            if (!Preferences.Default.ContainsKey(LAST_PHOTO_TIME)) { return false; }
            if (!Preferences.Default.ContainsKey(RESET_TIME)) { return false; }
            if (!Preferences.Default.ContainsKey(ONGOING_REMINDER)) { return false; }
            if (!Preferences.Default.ContainsKey(REMINDER_ENABLED)) { return false; }
            if (!Preferences.Default.ContainsKey(RESET_TIME)) { return false; }
            return true;
        }
    }
}