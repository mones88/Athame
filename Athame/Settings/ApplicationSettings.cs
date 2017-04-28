using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Athame.Settings
{
    public class SettingsManager<T> where T : new()
    {
        private readonly string settingsPath;
        private readonly JsonSerializerSettings SerializerSettings;

        public T Settings { get; private set; }

        public SettingsManager(string settingsPath)
        {
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
            if (!File.Exists(settingsPath))
            {
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
                catch (JsonSerializationException)
                {
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
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(Settings, SerializerSettings));
        }
    }
}
