using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Photolog.Page
{
    public partial class WelcomePage
    {

        private string GetStyleClass()
        {
            return Done ? "animate__bounceOutDown" : "animate__backInDown";
        }

    }
}
