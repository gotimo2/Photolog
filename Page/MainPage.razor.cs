using Microsoft.AspNetCore.Components;

namespace Photolog.Page
{
    public partial class MainPage
    {

        [Inject]
        private NavigationManager NavManager { get; set; }

        private bool HasHadWelcome = true;


        protected override Task OnInitializedAsync()
        {
            if (!HasHadWelcome)
            {
                NavManager.NavigateTo("/welcome");
            }

            return base.OnInitializedAsync();
        }

        private void OpenCamera()
        {
            NavManager.NavigateTo("/camera");
        }

    }
}
