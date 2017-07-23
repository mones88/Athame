using Athame.PluginAPI.Service;

namespace Athame.DownloadAndTag
{
    public static class ServiceMediaIdExtensions
    {
        public static ServiceMediaId GetSmid(this IMediaCollection collection, string serviceName)
        {
            return new ServiceMediaId(serviceName, collection);
        }

        
    }
}
