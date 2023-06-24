using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Page
{
    public partial class SettingsPage
    {
        [Inject]
        private NavigationManager NavManager { get; set; }
        
        private bool Done = false;


        private string GetStyleClass() => Done ? "animate__fadeInDown" : "animate__fadeOutUp";

        private async Task Back()
        {
            Done = true;
            StateHasChanged();
            await Task.Delay(1000);
            NavManager.NavigateTo("/");
        }
    }
}
