using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Downloader;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a picture for a media item.
    /// </summary>
    public abstract class Picture
    {

        /// <summary>
        /// The file type of the picture.
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// Retrieves the largest version of the picture available.
        /// </summary>
        /// <returns>A byte array of the picture's data.</returns>
        public abstract Task<byte[]> GetLargestVersionAsync();

        /// <summary>
        /// Whether or not a thumbnail size of the picture is available.
        /// </summary>
        public abstract bool IsThumbnailAvailable { get; }

        /// <summary>
        /// Retrieves a thumbnail version of the picture. A thumbnail should typically be between 80 to 200 pixels square.
        /// </summary>
        /// <returns>A byte array of the thumbnail picture's data.</returns>
        public abstract Task<byte[]> GetThumbnailVersionAsync();
    }
}
