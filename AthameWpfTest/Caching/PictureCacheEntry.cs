using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Athame.PluginAPI.Service;

namespace AthameWPF.Caching
{
    public class PictureCacheEntry
    {
        public Picture OriginalPicture { get; set; }

        public byte[] FullSize { get; private set; }

        public BitmapImage XamlFullSizeBitmap { get; private set; }

        public byte[] Thumbnail { get; private set; }

        public BitmapImage XamlThumbnailBitmap { get; private set; }

        private readonly SemaphoreSlim fullSizeSemaphore = new SemaphoreSlim(1, 1);

        private BitmapImage LoadBitmapImageFromBytes(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.Default;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public async Task GetFullSizeAsync()
        {
            await fullSizeSemaphore.WaitAsync();
            if (FullSize != null)
            {
                fullSizeSemaphore.Release();
                return;
            }
            try
            {
                FullSize = await OriginalPicture.GetLargestVersionAsync();
                XamlFullSizeBitmap = LoadBitmapImageFromBytes(FullSize);
            }
            finally
            {
                fullSizeSemaphore.Release();
            }
        }

        private readonly SemaphoreSlim thumbnailSemaphore = new SemaphoreSlim(1, 1);

        public async Task GetThumbnailAsync()
        {
            await thumbnailSemaphore.WaitAsync();
            if (Thumbnail != null)
            {
                thumbnailSemaphore.Release();
                return;
            }
            try
            {
                Thumbnail = await OriginalPicture.GetThumbnailVersionAsync();
                XamlThumbnailBitmap = LoadBitmapImageFromBytes(Thumbnail);
            }
            finally
            {
                thumbnailSemaphore.Release();
            }
        }
    }
}
