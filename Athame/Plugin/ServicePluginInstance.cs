using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;
using Athame.Settings;

namespace Athame.Plugin
{
    public class ServicePluginInstance : PluginInstance
    {
        public SettingsFile SettingsFile { get; set; }
        public MusicService Service { get; set; }
    }
}
