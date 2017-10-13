using System.IO;
using Athame.PluginAPI.Service;

namespace Athame.DownloadAndTag
{
    public class EnqueuedCollection
    {
        public string Destination { get; set; }
        public string PathFormat { get; set; }
        public MusicService Service { get; set; }
        public IMediaCollection MediaCollection { get; set; }
    }
}
