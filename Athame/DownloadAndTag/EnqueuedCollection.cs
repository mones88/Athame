using System.IO;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;
using Athame.Utils;

namespace Athame.DownloadAndTag
{
    public class EnqueuedCollection
    {
        public string Destination { get; set; }
        public string PathFormat { get; set; }
        public MusicService Service { get; set; }
        public IMediaCollection MediaCollection { get; set; }
        public MediaType Type { get; set; }
        public string GetRelativePath(TrackFile trackFile)
        {
            return trackFile.GetPath(PathFormat, MediaCollection);
        }

        public string GetPath(TrackFile trackFile)
        {
            return Path.Combine(Destination, GetRelativePath(trackFile));
        }
    }
}
