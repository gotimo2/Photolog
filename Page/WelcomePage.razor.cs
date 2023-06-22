using System.Diagnostics;

namespace Photolog.Page
{
    public partial class WelcomePage
    {
        private bool clickedButton = false;

        private void Click()
        {
            clickedButton = true;
            Console.WriteLine("button clicked");
            StateHasChanged();
        }

        private string GetStyleClass()
        {
            return clickedButton ? "animate__bounceOutDown" : "animate__backInDown";
        }

    }
}
