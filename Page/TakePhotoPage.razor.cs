using Microsoft.AspNetCore.Components;
using NativeMedia;
using Photolog.Helpers;

namespace Photolog.Page
{
    public partial class TakePhotoPage
    {
        [Inject]
        private NavigationManager NavManager { get; set; }

        private string ImageSource { get; set; }

        private FileResult LastPhoto { get; set; }

        private bool Done { get; set; } = false; 

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
                ErrorHolder.CurrentError = "This device cannot take a photo.";
                NavManager.NavigateTo("/error");
                return false;
            }

            if (await PermissionManager.getCameraPermissions() == false)
            {
                ErrorHolder.CurrentError = "The app has no permission to use the camera. Go to your device's settings to allow Photolog to access the camera.";
                NavManager.NavigateTo("/error");
                return false;
            }

            if (await PermissionManager.getStorageReadPermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to read storage. Go to your device's settings and allow photolog to access storage.";
                NavManager.NavigateTo("/error");
                return false;
            }

            if (await PermissionManager.getStorageWritePermissions() == false)
            {
                ErrorHolder.CurrentError = "Photolog has no permissions to write to storage. Go to your device's settings and allow photolog to access storage.";
                NavManager.NavigateTo("/error");
                return false;
            }
            return true;
        }

        private async Task SaveToGallery()
        {
            Done = true;
            StateHasChanged();
            var task = SaveToGallery(LastPhoto);
            Task[] taskArray = new Task[] { task, Task.Delay(1000) };
            await Task.WhenAll(taskArray);
            Preferences.Default.Set(PreferencesHelper.LAST_PHOTO_TIME, DateTime.Now);
            NotificationScheduler.closeNotification();

            var time = TimeOnly.Parse(Preferences.Default.Get(PreferencesHelper.RESET_TIME, "00:00:00"));
            await NotificationScheduler.scheduleNotification(DateTime.Now.Add(NotificationScheduler.TimeUntilNotification()), true);
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
            ImageSource = Convert.ToBase64String(lastImageBytes);
            ImageSource = string.Format("data:image/png;base64,{0}", ImageSource);
            return ImageSource;
        }


        private string GetPhotoStyle() => Done ? "animate__zoomOutLeft" : " animate__backInDown";

        private string GetButtonStyle() => Done ? "animate__zoomOutRight" : " animate__backInUp";

    }
}
