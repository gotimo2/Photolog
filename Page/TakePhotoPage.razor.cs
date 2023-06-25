using Microsoft.AspNetCore.Components;
using NativeMedia;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {

        private string ImageSource { get; set; }
        private FileResult LastPhoto { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await TakePhoto();
        }

        private async Task TakePhoto()
        {
            ImageSource = null;
            LastPhoto = null;
            Done = false;
            if (await EnsurePhotoPossible() == false) { return; } 
            await base.OnInitializedAsync();

            LastPhoto = await MediaPicker.CapturePhotoAsync();
            if (LastPhoto == null)
            {
                NavManager.NavigateTo("/error");
                ErrorHolder.CurrentError = "Failed to take a photo.";
                return;
            }
            ImageSource = await SaveToCache(LastPhoto);
            StateHasChanged();
        }


        private async Task<bool> EnsurePhotoPossible()
        {
            if (MediaPicker.Default.IsCaptureSupported == false)
            {
                await DisplayError("This device cannot take a photo.");
                return false;
            }

            if (await PermissionManager.GetCameraPermissions() == false)
            {
                await DisplayError("The app has no permission to use the camera. Go to your device's settings to allow Photolog to access the camera.");
                return false;
            }
          return true;
        }

        private async Task SaveToGallery()
        {
            Done = true;
            StateHasChanged();
            var task = GalleryHelper.SaveToGallery(Path.Combine(FileSystem.CacheDirectory, LastPhoto.FileName));
            Task[] taskArray = new Task[] { task, Task.Delay(1000) };
            await Task.WhenAll(taskArray);
            Preferences.Default.Set(PreferencesHelper.LAST_PHOTO_TIME, DateTime.Now);
            NotificationScheduler.CancelNotification();
            await NotificationScheduler.Schedule();
            NavManager.NavigateTo("/");
        }

        private async Task<string> SaveToCache(FileResult photo)
        {
            var CachedSource = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(CachedSource);
            await sourceStream.CopyToAsync(localFileStream);
            await localFileStream.DisposeAsync();
            var lastImageBytes = await File.ReadAllBytesAsync(CachedSource);
            ImageSource = Convert.ToBase64String(lastImageBytes);
            ImageSource = string.Format("data:image/png;base64,{0}", ImageSource);
            return ImageSource;
        }


        private string GetPhotoStyle() => Done ? "animate__zoomOutLeft" : " animate__backInDown";

        private string GetButtonStyle() => Done ? "animate__zoomOutRight" : " animate__backInUp";

    }
}
