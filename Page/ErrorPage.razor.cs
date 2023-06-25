using System;
using System.Collections.Generic;
using System.Linq;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class ErrorPage
    {

        private string errorMessage;

        private string GetStyleClass()
        {
            if (!Done)
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
