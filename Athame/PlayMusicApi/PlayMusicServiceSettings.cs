using Athame.PluginAPI.Service;
using GoogleMusicApi.Structure.Enums;

namespace Athame.PlayMusicApi
{
    public class PlayMusicServiceSettings
    {
        public StreamQuality StreamQuality { get; set; }

        public string Email { get; set; }
        public string SessionToken { get; set; }

        public PlayMusicServiceSettings()
        {
            StreamQuality = StreamQuality.High;
        }
    }
}
