using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Helpers
{
    public static class DailyPhotoHelper
    {
        public static bool photoReady()
        {
            var lastPhotoTime = Preferences.Default.Get<DateTime>(PreferencesHelper.LAST_PHOTO_TIME, DateTime.UnixEpoch);

            var selectedPhotoResetTime = TimeOnly.Parse(Preferences.Default.Get<string>(PreferencesHelper.RESET_TIME, "00:00:00"));

            var yesterday = DateTime.Now.AddDays(-1);

            var lastResetTime = yesterday.Date.Add(selectedPhotoResetTime.ToTimeSpan());

            if (lastPhotoTime > lastResetTime) { return false; };
            return true;
        }

        public static TimeSpan TimeUntilPhoto()
        {
            var selectedPhotoResetTime = TimeOnly.Parse(Preferences.Default.Get<string>(PreferencesHelper.RESET_TIME, "00:00:00"));
            return selectedPhotoResetTime.ToTimeSpan() - DateTime.Now.TimeOfDay;
        }
    }
}
