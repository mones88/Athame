using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Athame.Logging;
using Newtonsoft.Json;

namespace Athame.Settings
{
    public class SettingsManager<T> where T : new()
    {
        public const string Tag = "SettingsManager";

        private readonly string settingsPath;
        private readonly JsonSerializerSettings SerializerSettings;

        public T Settings { get; private set; }

        public SettingsManager(string settingsPath)
        {
            Log.Debug(Tag, "Init settings manager");
            this.settingsPath = settingsPath;
            SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full
            };
        }

        public void Load()
        {
            Log.Debug(Tag, "Load config");
            if (!File.Exists(settingsPath))
            {
                Log.Info(Tag, $"Create new config in {settingsPath}");
                Settings = new T();
            }
            else
            {
                try
                {
                    // Assign settings path to deserialised settings instance
                    Settings = JsonConvert.DeserializeObject<T>(File.ReadAllText(settingsPath),
                        SerializerSettings);
                }
                catch (JsonSerializationException ex)
                {
                    Log.WriteException(Level.Warning, Tag, ex, "Config could not be parsed, creating new");
#if DEBUG
                    throw;           
#endif
                    Settings = new T();
                    Save();
                }
            }
        }

        public void Save()
        {
            Log.Debug(Tag, $"Saving config to {settingsPath}");
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(Settings, SerializerSettings));
        }
    }
}
