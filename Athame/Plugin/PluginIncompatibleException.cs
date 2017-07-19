using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.Plugin
{
    public class PluginIncompatibleException : Exception
    {
        public PluginIncompatibleException(string message) : base(message) { }
    }
}
