using System;
using System.Reflection;
using Athame.PluginAPI;
using Athame.PluginAPI.Service;
using AthameWPF.Settings;

namespace AthameWPF.Plugin
{
    public class PluginInstance
    {
        public PluginInfo Info { get; set; }
        public IPlugin Plugin { get; set; }
        public PluginContext Context { get; set; }
        public Assembly Assembly { get; set; }
        public string AssemblyDirectory { get; set; }
        public string Name { get; set; }
        public Version AssemblyFileVersion { get; set; }
        public SettingsFile SettingsFile { get; set; }
        public MusicService Service => Plugin as MusicService;
        public IAuthenticatable AuthenticatableService => Service.AsAuthenticatable();

        public string AuthenticatableAccountName
        {
            get
            {
                var info = AuthenticatableService.Account;
                return info.DisplayName == null ? info.DisplayId : $"{info.DisplayName} ({info.DisplayId})";
            }
        }
    }
}
