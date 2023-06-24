using Microsoft.AspNetCore.Components;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class MainPage
    {

        [Inject]
        private NavigationManager NavManager { get; set; }


        private bool ClickedButton = false;


        protected override Task OnInitializedAsync()
        {

            if (!Preferences.Default.Get(PreferencesHelper.HAS_HAD_WELCOME, false))
            {
                PreferencesHelper.SetDefaultPreferences();
                NavManager.NavigateTo("/welcome");
            }

            return base.OnInitializedAsync();
        }

        private async Task OpenCamera()
        {
            await goTo("/camera");
        }

        private async Task GoToSettings()
        {
            await goTo("/settings");
        }

        private async Task goTo(string path)
        {
            ClickedButton = true;
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo(path);
        }

        private string GetStyleClass()
        {
            return ClickedButton ? "animate__zoomOut" : "animate__flipInX";
        }

    }
}
