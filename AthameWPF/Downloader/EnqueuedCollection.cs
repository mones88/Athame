using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF.Downloader
{
    public class EnqueuedCollection
    {
        public MusicService Service { get; set; }

        public IMediaCollection OriginalCollection { get; }

        public EnqueuedCollection(MusicService service, IMediaCollection collection)
        {
            Service = service;
            OriginalCollection = collection;
            Smid = new ServiceMediaId(Service.Info.Name, collection);
            if (collection.Tracks != null)
            {
                Tracks = EnqueuedTrack.FromCollection(service, collection.Tracks).ToList();
            }
        }

        public ServiceMediaId Smid { get; }

        public List<EnqueuedTrack> Tracks { get; set; }

        public static IEnumerable<EnqueuedCollection> FromCollection(MusicService service,
            IEnumerable<IMediaCollection> collection)
        {
            return collection.Select(mediaCollection => new EnqueuedCollection(service, mediaCollection));
        }
    }
}
