using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Athame.PluginAPI.Downloader;

namespace Athame.PluginAPI.Service
{
    public abstract class MusicService : IPlugin
    {

        /// <summary>
        /// Retrieves a track's downloadable form.
        /// </summary>
        /// <param name="track">The track to download.</param>
        /// <returns>A <see cref="TrackFile"/> containing file metadata and the URI of the track.</returns>
        public abstract Task<TrackFile> GetDownloadableTrackAsync(Track track);

        /// <summary>
        /// Retrieves a playlist. Note that it is up to the implementation to differentiate
        /// between different playlist types, if the music service specifies them.
        /// </summary>
        /// <param name="playlistId">The playlist ID to retrieve.</param>
        /// <returns>A playlist on success, null otherwise.</returns>
        public abstract Task<Playlist> GetPlaylistAsync(string playlistId);

        /// <summary>
        /// Retrieves the items in a playlist
        /// </summary>
        /// <param name="playlistId">The playlist's ID to retrieve.</param>
        /// <param name="itemsPerPage">The number of items to retrieve per page.</param>
        /// <returns>A paged list of tracks.</returns>
        public abstract PagedMethod<Track> GetPlaylistItems(string playlistId, int itemsPerPage);

        /// <summary>
        /// Parses a public-facing URL of a service, and returns the media type referenced and the identifier.
        /// </summary>
        /// <param name="url">A URL to parse.</param>
        /// <returns>A <see cref="UrlParseResult"/> containing a media type and ID.</returns>
        public abstract UrlParseResult ParseUrl(Uri url);

        /// <summary>
        /// Performs a text search and retrieves the results -- see <see cref="SearchResult"/> for what is returned.
        /// </summary>
        /// <param name="searchText">The text to search</param>
        /// <param name="typesToRetrieve">Which media to search for. This can be ignored for services which return all types regardless.</param>
        /// <param name="itemsPerPage">The number of items to retrieve per page.</param>
        /// <returns>A <see cref="SearchResult"/> containing top tracks, albums, or playlists.</returns>
        public abstract SearchResult Search(string searchText, MediaType typesToRetrieve, int itemsPerPage);

        /// <summary>
        /// Retrieves information for a specified artist.
        /// </summary>
        /// <param name="artistId">The artist's ID.</param>
        /// <returns>An artist.</returns>
        public abstract Task<Artist> GetArtistInfoAsync(string artistId);

        /// <summary>
        /// Retrieves the top tracks for an artist.
        /// </summary>
        /// <param name="artistId">The artist's ID.</param>
        /// <param name="itemsPerPage">How many items per page to retrieve.</param>
        /// <returns>A paged list of tracks.</returns>
        public abstract PagedMethod<Track> GetArtistTopTracks(string artistId, int itemsPerPage);

        /// <summary>
        /// Retrieves the albums released by an artist.
        /// </summary>
        /// <param name="artistId">The artist's ID</param>
        /// <param name="itemsPerPage">How many items per page to retrieve.</param>
        /// <returns>A paged list of albums.</returns>
        public abstract PagedMethod<Album> GetArtistAlbums(string artistId, int itemsPerPage);

        /// <summary>
        /// Retrieves an album.
        /// </summary>
        /// <param name="albumId">The album's identifier.</param>
        /// <param name="withTracks">Whether to return tracks or not. On some services, this may involve an extra API call. 
        /// Implementations are also allowed to return an object with tracks even if this is false.</param>
        /// <returns>An album, with or without tracks.</returns>
        public abstract Task<Album> GetAlbumAsync(string albumId, bool withTracks);

        /// <summary>
        /// Retrieves the metadata for a single track.
        /// </summary>
        /// <param name="trackId">The track's identifier.</param>
        /// <returns>A track.</returns>
        public abstract Task<Track> GetTrackAsync(string trackId);
        /// <summary>
        /// Returns a settings control to display in the settings form. Do not cache this in your implementation, as it is always disposed
        /// when the settings form closes.
        /// </summary>
        /// <returns>A settings control to display.</returns>
        public abstract Control GetSettingsControl();

        /// <summary>
        /// An object that holds persistent settings. Settings are deserialized from storage when the service is first initialized and 
        /// serialized when the user clicks "Save" on the settings form and when the application closes. Implementations should provide a
        /// "default" settings instance when there are no persisted settings available.
        /// </summary>
        public abstract object Settings { get; set; }

        /// <summary>
        /// The base URI of the service. Entered URIs are compared on the Scheme and Host properties of each base URI, and if they match,
        /// <see cref="ParseUrl"/> is called.
        /// </summary>
        public abstract Uri[] BaseUri { get; }

        /// <summary>
        /// Returns a downloader for this service. The default implementation is <see cref="HttpDownloader"/>,
        /// but this method may be overridden with a custom downloader that implements <see cref="IDownloader"/>.
        /// </summary>
        /// <returns>A new concrete implementation of <see cref="IDownloader"/>.</returns>
        public virtual IDownloader GetDownloader(TrackFile t)
        {
            return new HttpDownloader();
        }

        /// <summary>
        /// Provides information about a plugin.
        /// </summary>
        public abstract PluginInfo Info { get; }

        /// <summary>
        /// The API version the plugin implements.
        /// </summary>
        public abstract int ApiVersion { get; }

        /// <summary>
        /// Called when the plugin is loaded.
        /// </summary>
        /// <param name="application">The current host application instance.</param>
        /// <param name="pluginContext">Information about the plugin's location.</param>
        public abstract void Init(AthameApplication application, PluginContext pluginContext);
    }
}
