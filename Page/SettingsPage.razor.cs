using Microsoft.AspNetCore.Components;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class SettingsPage
    {
        private TimeOnly ReminderTime { get; set; }

        private TimeOnly ResetTime { get; set; }

        private bool EnableReminder { get; set; }

        private bool OngoingReminder { get; set; }

        protected override Task OnInitializedAsync()
        {
            ReminderTime = TimeOnly.Parse(Preferences.Default.Get(PreferencesHelper.REMINDER_TIME, "00:00:00"));
            ResetTime = TimeOnly.Parse(Preferences.Default.Get(PreferencesHelper.RESET_TIME, "00:00:00"));

            EnableReminder = Preferences.Default.Get<bool>(PreferencesHelper.REMINDER_ENABLED, true);
            OngoingReminder = Preferences.Default.Get<bool>(PreferencesHelper.ONGOING_REMINDER, false);

            return base.OnInitializedAsync();
        }

        private async Task SaveSettings()
        {
            Preferences.Set(PreferencesHelper.RESET_TIME, ResetTime.ToString());
            Preferences.Set(PreferencesHelper.REMINDER_TIME, ReminderTime.ToString());
            Preferences.Set(PreferencesHelper.REMINDER_ENABLED, EnableReminder);
            Preferences.Set(PreferencesHelper.ONGOING_REMINDER, OngoingReminder);
            await NotificationScheduler.ReSchedule();
        }

        private string GetStyleClass() => Done ? "animate__fadeOutUp" : "animate__fadeInDown";

        private async Task Back()
        {
            await SaveSettings();
            await GoToMainMenu();
        }

        private async Task GoToCredits()
        {
            await SaveSettings();
            await GoToPage("/credits");
        }
    }
}
