using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF.Downloader
{
    public class EnqueuedPlaylist : EnqueuedCollection<Playlist>
    {
        public EnqueuedPlaylist(MusicService service, Playlist collection) : base(service, collection)
        {
        }
    }
}
