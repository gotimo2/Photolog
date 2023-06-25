using Microsoft.AspNetCore.Components;
using Photolog.Helpers;

namespace Photolog.Page
{
    public abstract class BasePage : ComponentBase
    {
        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected bool Done { get; set; } = false;

        protected async Task GoToPage(string path)
        {
            Done = true;
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo(path);
        }

        protected async Task GoToMainMenu()
        {
            await GoToPage("/");
        }

        protected async Task DisplayError(string error)
        {
            ErrorHolder.CurrentError = error;
            await GoToPage("/error");
        }
    }
}
