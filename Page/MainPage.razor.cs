using Microsoft.AspNetCore.Components;

namespace Photolog.Page
{
    public partial class MainPage
    {

        [Inject]
        private NavigationManager navManager { get; set; }

        private bool HasHadWelcome = false;


        protected override Task OnInitializedAsync()
        {
            if (!HasHadWelcome)
            {
                navManager.NavigateTo("/welcome");
            }

            return base.OnInitializedAsync();
        }

    }
}
