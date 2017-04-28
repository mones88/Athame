using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
