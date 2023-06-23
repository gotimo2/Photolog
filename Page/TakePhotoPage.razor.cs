using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {

        protected override async Task OnInitializedAsync()
        {

            if (!MediaPicker.Default.IsCaptureSupported) {
                throw new InvalidOperationException("This device cannot take a photo!");
            }

            if (await PermissionManager.getCameraPermissions() == false)
            {
                throw new InvalidOperationException("No camera permissions!");
            }

            if (await PermissionManager.getStorageReadPermissions() == false)
            {
                throw new InvalidOperationException("No storage read permissions!");
            }
            
            if (await PermissionManager.getStorageWritePermissions() == false)
            {
                throw new InvalidOperationException("No storage write permissions!");
            }

            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            await base.OnInitializedAsync();
        }

    }
}
