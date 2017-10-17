using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF.Downloader
{
    public class EnqueuedAlbum : EnqueuedCollection<Album>
    {
        public EnqueuedAlbum(MusicService service, Album collection) : base(service, collection)
        {
        }
    }
}
