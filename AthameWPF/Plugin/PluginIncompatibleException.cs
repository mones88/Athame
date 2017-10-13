using System;

namespace AthameWPF.Plugin
{
    public class PluginIncompatibleException : Exception
    {
        public PluginIncompatibleException(string message) : base(message) { }
    }
}
