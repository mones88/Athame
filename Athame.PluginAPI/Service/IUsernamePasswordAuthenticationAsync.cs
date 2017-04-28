using System.Collections.Generic;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public interface IUsernamePasswordAuthenticationAsync : IAuthenticatable
    {
        Task<bool> AuthenticateAsync(string username, string password, bool rememberUser);
        string SignInHelpText { get; }
        IReadOnlyCollection<SignInLink> SignInLinks { get; }
    }
}