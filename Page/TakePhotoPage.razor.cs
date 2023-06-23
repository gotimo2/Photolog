using Microsoft.AspNetCore.Components;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {
        [Inject]
        private NavigationManager navManager { get; set; }

        protected override async Task OnInitializedAsync()
        {

            if (!MediaPicker.Default.IsCaptureSupported) {
                ErrorHolder.CurrentError = "This device cannot take a photo.";
                navManager.NavigateTo("/error");
            }

            if (await PermissionManager.getCameraPermissions() == false)
            {
                ErrorHolder.CurrentError = "The app has no permission to use the camera. Go to settings to allow Photolog to access the camera.";
                navManager.NavigateTo("/error");
            }

            if (await PermissionManager.getStorageReadPermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to read storage. Go to your device's settings to allow this.";
                navManager.NavigateTo("/error");
            }
            
            if (await PermissionManager.getStorageWritePermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to write storage. Go to your device's to allow this.";
                navManager.NavigateTo("/error");
            }

            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            await base.OnInitializedAsync();
        }

    }
}
