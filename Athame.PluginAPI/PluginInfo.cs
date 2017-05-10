using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI
{
    public class PluginInfo
    {
        /// <summary>
        /// The plugin's name. Required.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The plugin's description. Optional.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The plugin's author. Optional.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// The plugin's homepage. Optional.
        /// </summary>
        public Uri Website { get; set; }
    }
}
