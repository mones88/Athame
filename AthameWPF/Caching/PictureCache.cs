using System;
using System.Runtime.Caching;
using System.Windows.Media.Imaging;
using Athame.PluginAPI.Service;

namespace AthameWPF.Caching
{
    public static class PictureCache
    {
        private static readonly MemoryCache BackingCache = MemoryCache.Default;

        public static void AddPicture(string mediaId, Picture picture)
        {
            BackingCache.Add(mediaId, new PictureCacheEntry(picture), ObjectCache.InfiniteAbsoluteExpiration);
        }

        public static bool HasPicture(string mediaId)
        {
            return BackingCache.Contains(mediaId);
        }

        public static PictureCacheEntry GetPicture(string mediaId)
        {
            return (PictureCacheEntry)BackingCache.Get(mediaId);
        }
    }
}
