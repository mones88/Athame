using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace Athame.PluginAPI
{
    /// <summary>
    /// Represents the base of all plugins.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// The plugin's name. Required.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The plugin's description. Optional.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The plugin's author. Optional.
        /// </summary>
        string Author { get; }
        /// <summary>
        /// The plugin's homepage. Optional.
        /// </summary>
        Uri Website { get; }
        /// <summary>
        /// The plugin API version this plugin implements.
        /// </summary>
        PluginVersion ApiVersion { get; }
        /// <summary>
        /// Called when the plugin is initialized.
        /// </summary>
        /// <param name="application">Info and methods for interacting with the host application</param>
        /// <param name="pluginContext">Information about how the plugin is loaded</param>
        void Init(AthameApplication application, PluginContext pluginContext);
    }
}
