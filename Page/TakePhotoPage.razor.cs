using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using Photolog.Helpers;
using System.Runtime.CompilerServices;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {
        [Inject]
        private NavigationManager NavManager { get; set; }

        private string imageSource { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await TakePhoto();
        }

        private async Task TakePhoto()
        {
            imageSource = null;
            await EnsurePhotoPossible();
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo == null)
            {
                NavManager.NavigateTo("/");
                return;
            }
            imageSource = await SaveToCache(photo);
            StateHasChanged();
            await base.OnInitializedAsync();
        }



        private async Task EnsurePhotoPossible()
        {
            if (MediaPicker.Default.IsCaptureSupported == false)
            {
                ErrorHolder.CurrentError = "This device cannot take a photo.";
                NavManager.NavigateTo("/error");
            }

            if (await PermissionManager.getCameraPermissions() == false)
            {
                ErrorHolder.CurrentError = "The app has no permission to use the camera. Go to settings to allow Photolog to access the camera.";
                NavManager.NavigateTo("/error");
            }

            if (await PermissionManager.getStorageReadPermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to read storage. Go to your device's settings to allow this.";
                NavManager.NavigateTo("/error");
            }

            if (await PermissionManager.getStorageWritePermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to write storage. Go to your device's to allow this.";
                NavManager.NavigateTo("/error");
            }
        }

        private async Task<string> SaveToCache(FileResult photo)
        {
            var CachedSource = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(CachedSource);
            await sourceStream.CopyToAsync(localFileStream);
            await localFileStream.FlushAsync();
            localFileStream.Close();
            var imageBytes = File.ReadAllBytes(CachedSource);
            imageSource = Convert.ToBase64String(imageBytes);
            imageSource = string.Format("data:image/png;base64,{0}", imageSource);
            return imageSource;
        }

    }
}
