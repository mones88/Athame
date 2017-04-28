using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI;
using Athame.PluginAPI.Service;

namespace Athame.Plugin
{

    public class PluginManager
    {
        public const string PluginDir = "Plugins";
        public const string PluginDllPrefix = "AthamePlugin.";

        private readonly string pluginDir;

        public PluginManager(string pluginDir)
        {
            this.pluginDir = pluginDir;
            Directory.CreateDirectory(pluginDir);
            Plugins = new List<IPlugin>();
            // !! NOTE !! This will be invoked if an assembly, for whatever reason, is loaded during
            // the plugin load process.
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                // If we are currently loading plugin assemblies
                if (!isLoading) return null;

                // If for some reason an assembly to resolve doesn't have a file path
                if (args.RequestingAssembly.Location == null) return null;

                // Parse name, get parent directory from requesting assembly's location, then
                // build a path.
                var name = new AssemblyName(args.Name);
                var parentDir = Directory.GetParent(args.RequestingAssembly.Location).FullName;
                var referencedDllPath = Path.Combine(parentDir, name.Name + ".dll");

                // Load the assembly no matter what.
                return Assembly.LoadFile(referencedDllPath);
            };
        }

        public List<IPlugin> Plugins { get; }

        public event EventHandler<PluginLoadExceptionEventArgs> LoadException;

        private Assembly[] loadedAssemblies;
        private bool isLoading;

        private bool IsAlreadyLoaded(string assemblyFullName)
        {
            return loadedAssemblies.Any(assembly => assembly.FullName == assemblyFullName);
        }

        public void LoadAll(Dictionary<string, object> savedSettings)
        {
            // Cache current AppDomain loaded assemblies
            loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Plugins are stored in format {PluginDir}/{PluginName}/AthamePlugin.*.dll
            var subDirs = Directory.GetDirectories(pluginDir);
            var pluginDlls = new List<string>();
            foreach (var subDir in subDirs)
            {
                pluginDlls.AddRange(Directory.GetFiles(subDir, $"{PluginDllPrefix}*.dll"));
            }
            isLoading = true;
            // Load and activate all plugins
            var assemblies = from dllPath in pluginDlls select Assembly.LoadFile(dllPath);
            foreach (var assembly in assemblies)
            {
                try
                {
                    if (assembly == null) continue;
                    if (IsAlreadyLoaded(assembly.FullName)) continue;

                    var types = assembly.GetExportedTypes();
                    // Only filter for types which can be instantiated and implement IPlugin somehow.
                    var implementingType = types.FirstOrDefault(
                        type =>
                            !type.IsInterface &&
                            !type.IsAbstract &&
                            type.GetInterface(nameof(IPlugin)) != null);
                    if (implementingType == null)
                    {
                        throw new PluginLoadException("No exported types found implementing IPlugin.",
                            assembly.Location);
                    }
                    // Activate base plugin
                    var plugin = (IPlugin) Activator.CreateInstance(implementingType);

                    // If it's a service plugin, add it to main service collection
                    var service = plugin as MusicService;
                    if (service == null) return;

                    // Restore the config, or set the config to the default value
                    if (savedSettings.ContainsKey(service.Name) && savedSettings[service.Name] != null)
                    {
                        service.Settings = savedSettings[service.Name];
                    }
                    else
                    {
                        savedSettings[service.Name] = service.Settings;
                    }

                    // Call Init
                    plugin.Init(Program.DefaultApp,
                        new PluginContext {PluginDirectory = Directory.GetParent(assembly.Location).FullName});
                    Plugins.Add(plugin);
                    AddService(service);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debugger.Break();
#endif
                    var eventArgs = new PluginLoadExceptionEventArgs {Exception = ex, Continue = true};
                    LoadException?.Invoke(this, eventArgs);
                    if (!eventArgs.Continue) return;
                }
            }
            isLoading = false;
        }

        private readonly HashSet<MusicService> services = new HashSet<MusicService>();
        private readonly Dictionary<Uri, MusicService> servicesByUris = new Dictionary<Uri, MusicService>();

        private void AddService(MusicService service)
        {
            services.Add(service);
            foreach (var uri in service.BaseUri)
            {
                servicesByUris.Add(uri, service);
            }
        }

        public MusicService GetService(string name)
        {
            return (from service in services
                where service.Name == name
                select service).FirstOrDefault();
        }

        public MusicService GetServiceByBaseUri(Uri baseUri)
        {
            return (from s in servicesByUris
                    where s.Key.Scheme == baseUri.Scheme && s.Key.Host == baseUri.Host
                    select s.Value).FirstOrDefault();
        }

        public IEnumerable<MusicService> ServicesEnumerable()
        {
            return services.AsEnumerable();
        }

    }
}
