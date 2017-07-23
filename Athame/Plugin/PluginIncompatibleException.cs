using System;

namespace Athame.Plugin
{
    public class PluginIncompatibleException : Exception
    {
        public PluginIncompatibleException(string message) : base(message) { }
    }
}
