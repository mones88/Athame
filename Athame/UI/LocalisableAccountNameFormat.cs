using Athame.PluginAPI.Service;

namespace Athame.UI
{
    public class LocalisableAccountNameFormat
    {
        public static string GetFormattedName(AccountInfo info)
        {
            return info.DisplayName == null ? info.DisplayId : $"{info.DisplayName} ({info.DisplayId})";
        }
    }
}
