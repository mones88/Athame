using System;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents the result of a URL parsed with <see cref="MusicService.ParseUrl"/>.
    /// </summary>
    public class UrlParseResult
    {
        /// <summary>
        /// The original <see cref="Uri"/> that was parsed.
        /// </summary>
        public Uri OriginalUri { get; set; }
        /// <summary>
        /// The media type this URL refers to.
        /// </summary>
        public MediaType Type { get; set; }
        /// <summary>
        /// The ID this URL refers to. If <see cref="Type"/> is <see cref="MediaType.Unknown"/>,
        /// this should be null to indicate the URL did not point to a valid resource.
        /// </summary>
        public string Id { get; set; }
    }
}
