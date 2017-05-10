using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI;
using Athame.Settings;

namespace Athame.Plugin
{
    public class PluginInstance
    {
        public PluginInfo Info { get; set; }
        public IPlugin Plugin { get; set; }

        public PluginContext Context { get; set; }
    }
}
