using System.Collections.Generic;
using System.Linq;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;

namespace AthameWPF.Downloader
{
    public class EnqueuedTrack
    {
        public MusicService Service { get; set; }
        public EnqueuedTrack(MusicService service, Track originalTrack)
        {
            Service = service;
            OriginalTrack = originalTrack;
            Smid = new ServiceMediaId(Service.Info.Name, OriginalTrack);
        }
        public ServiceMediaId Smid { get; }
        public Track OriginalTrack { get; set; }
        public TrackFile TrackFile { get; set; }
        public decimal PercentComplete { get; set; }
        public DownloadState State { get; set; }

        public static IEnumerable<EnqueuedTrack> FromCollection(MusicService service, IEnumerable<Track> tracks)
        {
            return tracks.Select(track => new EnqueuedTrack(service, track));
        }

    }
}
