using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Photolog.Page
{
    public partial class WelcomePage
    {

        [Inject]
        private NavigationManager NavManager { get; set; }

        private bool clickedButton = false;

        private async Task Click()
        {
            clickedButton = true;
            Console.WriteLine("button clicked");
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo("/");
        }

        private string GetStyleClass()
        {
            return clickedButton ? "animate__bounceOutDown" : "animate__backInDown";
        }

    }
}
