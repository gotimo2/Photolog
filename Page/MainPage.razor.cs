using Microsoft.Maui.Platform;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class MainPage
    {
        private static bool PhotoReady() => DailyPhotoHelper.PhotoReady();

        private static string TimeUntilPhoto() => DailyPhotoHelper.TimeUntilPhoto().ToFormattedString("HH:mm");

        protected override async Task OnInitializedAsync()
        {

            if (!PreferencesHelper.HasHadWelcome)
            {
                PreferencesHelper.SetDefaultPreferences();
                NavManager.NavigateTo("/welcome");
            }
            _ = Task.Run(async () =>
            {
                while (!PhotoReady())
                {
                    await Task.Delay(30000);
                    StateHasChanged();
                }
                StateHasChanged();
            });
            await NotificationScheduler.ReSchedule();
            await base.OnInitializedAsync();
        }


        private async Task OpenCamera()
        {
            await GoToPage("/camera");
        }

        private async Task GoToSettings()
        {
            await GoToPage("/settings");
        }


        private string GetStyleClass()
        {
            return Done ? "animate__zoomOut" : "animate__flipInX";
        }

    }
}
