namespace Photolog.Page
{
    public partial class TakePhotoPage
    {

        protected override async Task OnInitializedAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported) {
                throw new InvalidOperationException("This device cannot take a photo!");
            }

            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            await base.OnInitializedAsync();
        }

    }
}
