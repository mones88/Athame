using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// The base interface for services that provide authentication. Do not directly implement this interface;
    /// implement either <see cref="IUsernamePasswordAuthenticationAsync"/> or <see cref="IAuthenticatableAsync"/>
    /// depending on your needs.
    /// </summary>
    public interface IAuthenticatable
    {
        /// <summary>
        /// The account associated with this service, or null if the user is signed out.
        /// </summary>
        AccountInfo Account { get; }
        /// <summary>
        /// Whether the service has a signed-in user or not.
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// Whether the service has a saved session that can be restored or not.
        /// </summary>
        bool HasSavedSession { get; }
        /// <summary>
        /// Restores the session based on the session details stored in settings.
        /// </summary>
        /// <returns>True if the restore succeeded, false otherwise.</returns>
        Task<bool> RestoreAsync();
        /// <summary>
        /// Clears any stored session details.
        /// </summary>
        void Reset();

        /// <summary>
        /// Gets the currently signed in user's saved tracks
        /// </summary>
        /// <param name="itemsPerPage">The number of items per page to retrieve at a time.</param>
        /// <returns></returns>
        PagedMethod<Track> GetUserSavedTracks(int itemsPerPage);

        /// <summary>
        /// Gets the currently signed in user's saved artists
        /// </summary>
        /// <param name="itemsPerPage">The number of items per page to retrieve at a time.</param>
        /// <returns></returns>
        PagedMethod<Artist> GetUserSavedArtists(int itemsPerPage);

        /// <summary>
        /// Gets the currently signed in user's saved albums
        /// </summary>
        /// <param name="itemsPerPage">The number of items per page to retrieve at a time.</param>
        /// <returns></returns>
        PagedMethod<Album> GetUserSavedAlbums(int itemsPerPage);

        /// <summary>
        /// Gets the currently signed in user's saved playlists
        /// </summary>
        /// <param name="itemsPerPage">The number of items per page to retrieve at a time.</param>
        /// <returns></returns>
        PagedMethod<Playlist> GetUserSavedPlaylists(int itemsPerPage);
    }
}