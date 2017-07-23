using System;
using System.Collections.Generic;
using System.IO;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;
using CenterCLR;

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
            return GetBasicPath(track, pathFormat, Dictify.ObjectToDictionary(track));
        }

        public static string GetPath(this TrackFile trackFile, string pathFormat, Dictionary<string, object> vars)
        {
            var cleanedFilePath = trackFile.Track.GetBasicPath(pathFormat, vars);
            return trackFile.FileType.Append(cleanedFilePath);
        }

        public static string GetBasicPath(this Track track, string pathFormat, Dictionary<string, object> vars)
        {
            
            // Hacky method to clean the file path
            var formatStrComponents = pathFormat.Split(Path.DirectorySeparatorChar);
            var newFormat = String.Join("\0", formatStrComponents);
            var finalPath = Named.Format(newFormat, vars);
            return PathHelpers.CleanPath(finalPath);
        }
    }
}
