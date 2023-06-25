using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Helpers
{
    public static class DailyPhotoHelper
    {
        public static bool PhotoReady()
        {
            return true;
        }

        public static TimeSpan TimeUntilPhoto()
        {
            return PreferencesHelper.ResetTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }

        public static TimeSpan TimeUntilNotification()
        {
            return PreferencesHelper.ReminderTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }
    }
}
