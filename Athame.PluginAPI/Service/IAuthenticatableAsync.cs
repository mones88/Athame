using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public interface IAuthenticatableAsync : IAuthenticatable
    {
        Task<bool> AuthenticateAsync();
    }
}