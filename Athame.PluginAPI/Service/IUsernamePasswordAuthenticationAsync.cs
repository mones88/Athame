using System.Collections.Generic;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents an authentication method where the user provides a username and password.
    /// </summary>
    public interface IUsernamePasswordAuthenticationAsync : IAuthenticatable
    {
        /// <summary>
        /// Attempts authentication with the specified username and password.
        /// </summary>
        /// <param name="username">The username to use.</param>
        /// <param name="password">The password to use.</param>
        /// <param name="rememberUser">Whether the user's session is saved in settings.</param>
        /// <returns></returns>
        Task<bool> AuthenticateAsync(string username, string password, bool rememberUser);
        /// <summary>
        /// Helpful text to show to the user while entering their credentials.
        /// </summary>
        string SignInHelpText { get; }
        /// <summary>
        /// A collection of links to show to the user that may assist them with signing in.
        /// </summary>
        IReadOnlyCollection<SignInLink> SignInLinks { get; }
    }
}