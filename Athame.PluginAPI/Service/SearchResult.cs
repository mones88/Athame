using System.Collections.Generic;

namespace Athame.PluginAPI.Service
{
    public class SearchResult
    {
        public PagedMethod<Track> TopTracks { get; set; }
        public PagedMethod<Album> Albums { get; set; }
        public PagedMethod<Artist> Artists { get; set; }
        public PagedMethod<Playlist> Playlists { get; set; }
    }
}
