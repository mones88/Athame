using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public class ServiceMediaId
    {
        public string ServiceName { get; set; }
        public MediaType MediaType { get; set; }
        public string Id { get; set; }

        private const char Separator = '-';

        public ServiceMediaId(string serviceName, Track track)
        {
            ServiceName = serviceName;
            MediaType = MediaType.Track;
            Id = track.Id;
        }

        public ServiceMediaId(string serviceName, IMediaCollection collection)
        {
            ServiceName = serviceName;
            if (collection is Album)
            {
                MediaType = MediaType.Album;
            }
            else if (collection is Playlist)
            {
                MediaType = MediaType.Playlist;
            }
            else if (collection is SingleTrackCollection)
            {

                MediaType = MediaType.Track;
            }
            else
            {
                MediaType = MediaType.Unknown;
            }
            Id = collection.Id;
        }

        private ServiceMediaId() { }

        public ServiceMediaId(string serviceName, Artist artist)
        {
            ServiceName = serviceName;
            MediaType = MediaType.Artist;
            Id = artist.Id;
        }

        public static ServiceMediaId Parse(string id)
        {
            var result = id.Split(Separator);
            if (result.Length != 3)
            {
                throw new ArgumentException("id");
            }
            MediaType typeParseResult;
            if (!Enum.TryParse(result[1], out typeParseResult))
            {
                throw new ArgumentException("id");
            }
            return new ServiceMediaId
            {
                ServiceName = result[0],
                MediaType = typeParseResult,
                Id = result[2]
            };
        }

        public override string ToString()
        {
            return String.Join(Separator.ToString(), ServiceName, MediaType, Id);
        }
    }
}
