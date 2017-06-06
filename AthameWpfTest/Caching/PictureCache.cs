using System;
using System.Runtime.Caching;
using System.Windows.Media.Imaging;
using Athame.PluginAPI.Service;

namespace AthameWPF.Caching
{
    public static class PictureCache
    {
        private static readonly MemoryCache backingCache = MemoryCache.Default;

        public static void AddPicture(string mediaId, Picture picture)
        {
            backingCache.Add(mediaId, new PictureCacheEntry {OriginalPicture = picture}, DateTimeOffset.MaxValue);
        }

        public static bool HasPicture(string mediaId)
        {
            return backingCache.Contains(mediaId);
        }

        public static PictureCacheEntry GetPicture(string mediaId)
        {
            return (PictureCacheEntry)backingCache.Get(mediaId);
        }
    }
}
