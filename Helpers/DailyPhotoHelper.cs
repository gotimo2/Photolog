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

        public static TimeSpan TimeUntilPhoto()
        {
            TimeSpan resetTime = PreferencesHelper.ResetTime.ToTimeSpan();
            DateTime now = DateTime.Now;
            DateTime resetDateTime = new(now.Year, now.Month, now.Day, resetTime.Hours, resetTime.Minutes, resetTime.Seconds);
            if (resetDateTime <= now)
            {
                resetDateTime = resetDateTime.AddDays(1);
            }
            return resetDateTime - now;
        }


    }
}
