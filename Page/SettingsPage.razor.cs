using Microsoft.AspNetCore.Components;

namespace Photolog.Page
{
    public partial class SettingsPage
    {
        [Inject]
        private NavigationManager NavManager { get; set; }
        
        private bool Done = false;

        private string GetStyleClass() => Done ? "animate__fadeOutUp" : "animate__fadeInDown";

        private async Task Back()
        {
            Done = true;
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo("/");
        }
    }
}
