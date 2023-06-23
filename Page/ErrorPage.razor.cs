using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class ErrorPage
    {
        [Inject]
        private NavigationManager navManager { get; set; }

        private bool acknowledged { get; set; } = false;

        private string errorMessage;

        private void Click()
        {
            acknowledged = true;
            navManager.NavigateTo("/");
        }

        private string GetStyleClass()
        {
            if (!acknowledged)
            {
                return "animate__backInDown";
            }
            else
            {
                return "animate__backOutDown";
            }
        }

        protected override Task OnInitializedAsync()
        {
            this.errorMessage = ErrorHolder.CurrentError;
            return base.OnInitializedAsync();
        }

    }
}
