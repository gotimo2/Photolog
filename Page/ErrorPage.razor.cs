
/* Unmerged change from project 'Photolog (net7.0-windows10.0.19041.0)'
Before:
using System;
After:
using Photolog.Helpers;
using System;
*/

/* Unmerged change from project 'Photolog (net7.0-ios)'
Before:
using System;
After:
using Photolog.Helpers;
using System;
*/

/* Unmerged change from project 'Photolog (net7.0-maccatalyst)'
Before:
using System;
After:
using Photolog.Helpers;
using System;
*/
using
/* Unmerged change from project 'Photolog (net7.0-windows10.0.19041.0)'
Before:
using System.Linq;
using Photolog.Helpers;
After:
using System.Linq;
*/

/* Unmerged change from project 'Photolog (net7.0-ios)'
Before:
using System.Linq;
using Photolog.Helpers;
After:
using System.Linq;
*/

/* Unmerged change from project 'Photolog (net7.0-maccatalyst)'
Before:
using System.Linq;
using Photolog.Helpers;
After:
using System.Linq;
*/
Photolog.Helpers;

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
