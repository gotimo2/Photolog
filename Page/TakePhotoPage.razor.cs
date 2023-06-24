using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using NativeMedia;
using Photolog.Helpers;
using System.Runtime.CompilerServices;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {
        [Inject]
        private NavigationManager NavManager { get; set; }

        private string imageSource { get; set; }

        private FileResult lastPhoto { get; set; }

        private bool done { get; set; } = false; 

        protected override async Task OnInitializedAsync()
        {
            await TakePhoto();
        }

        private async Task TakePhoto()
        {
            imageSource = null;
            lastPhoto = null;
            done = false;
            await EnsurePhotoPossible();
            lastPhoto = await MediaPicker.CapturePhotoAsync();
            if (lastPhoto == null)
            {
                NavManager.NavigateTo("/error");
                ErrorHolder.CurrentError = "Failed to take a photo.";
                return;
            }
            imageSource = await SaveToCache(lastPhoto);
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

        private async Task SaveToGallery()
        {
            done = true;
            StateHasChanged();
            var task = SaveToGallery(lastPhoto);
            Task[] taskArray = new Task[] { task, Task.Delay(1000) };
            await Task.WhenAll(taskArray);

            NavManager.NavigateTo("/");

        }




        private async Task SaveToGallery(FileResult photo)
        {
            await MediaGallery.SaveAsync(MediaFileType.Image, Path.Combine(FileSystem.CacheDirectory, photo.FileName));
        }

        private async Task<string> SaveToCache(FileResult photo)
        {
            var CachedSource = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(CachedSource);
            await sourceStream.CopyToAsync(localFileStream);
            await localFileStream.DisposeAsync();
            var lastImageBytes = await File.ReadAllBytesAsync(CachedSource);
            imageSource = Convert.ToBase64String(lastImageBytes);
            imageSource = string.Format("data:image/png;base64,{0}", imageSource);
            return imageSource;
        }


        private string GetPhotoStyle() => done ? "animate__zoomOutLeft" : " animate__backInDown";

        private string GetButtonStyle() => done ? "animate__zoomOutRight" : " animate__backInDown";

    }
}
