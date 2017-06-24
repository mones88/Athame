using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.Plugin
{
    public class PluginLoadExceptionEventArgs
    {
        public string PluginName { get; set; }
        public Exception Exception { get; set; }
        public bool Continue { get; set; }
    }
}
