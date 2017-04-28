using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public interface IAuthenticatable
    {
        AccountInfo Account { get; }
        bool IsAuthenticated { get; }
        bool HasSavedSession { get; }
        Task<bool> RestoreAsync();
        void Reset();
    }
}