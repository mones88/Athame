using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace Athame.Utils
{
    public static class MediaCollectionExtensions
    {
        public static int GetAvailableTracksCount(this IMediaCollection collection)
        {
            return collection.Tracks.Sum(track => track.IsDownloadable ? 1 : 0);
        }
    }
}
