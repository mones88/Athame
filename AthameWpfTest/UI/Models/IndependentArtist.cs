using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Models
{
    public class IndependentArtist
    {
        public MusicService Service { get; set; }

        public IndependentArtist(MusicService service, Artist originalArtist)
        {
            Service = service;
            OriginalArtist = originalArtist;
            Smid = new ServiceMediaId(Service.Info.Name, originalArtist);
        }

        public ServiceMediaId Smid { get; }

        public Artist OriginalArtist { get; }

        public static IEnumerable<IndependentArtist> FromCollection(MusicService service, IEnumerable<Artist> artists)
        {
            return artists.Select(artist => new IndependentArtist(service, artist));
        }
    }
}
