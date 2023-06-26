using NativeMedia;

namespace Photolog.Helpers
{
    public static class GalleryHelper
    {
        public async static Task SaveToGallery(string path)
        {
            await MediaGallery.SaveAsync(MediaFileType.Image, path);
        }
    }
}
