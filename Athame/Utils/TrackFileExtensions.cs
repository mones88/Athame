using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;

namespace Athame.Utils
{
    public static class TrackFileExtensions
    {

        /// <summary>
        /// Gets the path for a track, without the extension.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <param name="pathFormat">The path format.</param>
        /// <returns>A formatted path without an extension.</returns>
        public static string GetBasicPath(this Track track, string pathFormat)
        {
            return GetBasicPath(track, pathFormat, new StringObjectFormatter());
        }

        public static string GetPath(this TrackFile trackFile, string pathFormat, StringObjectFormatter formatter)
        {
            var cleanedFilePath = trackFile.Track.GetBasicPath(pathFormat, formatter);
            return trackFile.FileType.Append(cleanedFilePath);
        }

        public static string GetBasicPath(this Track track, string pathFormat, StringObjectFormatter formatter)
        {
            return formatter.FormatInstance(pathFormat, track,
                o => PathHelpers.CleanFilename(o.ToString()));
        }
    }
}
