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
            ClickedButton = true;
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo("/camera");
        }

        private string GetStyleClass()
        {
            return ClickedButton ? "animate__zoomOut" : "animate__flipInX";
        }

    }
}
