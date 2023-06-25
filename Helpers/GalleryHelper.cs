using NativeMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
