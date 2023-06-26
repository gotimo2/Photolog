using Microsoft.Maui.Controls.Internals;
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
            var momentOfReset = DateTime.Now.Date.Add(PreferencesHelper.ResetTime.ToTimeSpan());
            if (DateTime.Now > momentOfReset && PreferencesHelper.LastPhotoTime < momentOfReset) { return true; }
            return false;
        }

        public static TimeSpan TimeUntilPhoto() => PreferencesHelper.ResetTime.ToTimeSpan() - DateTime.Now.TimeOfDay;

    }
}
