using System;

namespace AthameWPF.Plugin
{
    public class PluginLoadExceptionEventArgs
    {
        public string PluginName { get; set; }
        public Exception Exception { get; set; }
        public bool Continue { get; set; }
    }
}
