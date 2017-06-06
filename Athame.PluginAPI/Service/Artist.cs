using System;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a music artist.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// The artist's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The artist's ID.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// This is obsolete. Use <see cref="Picture"/>.
        /// </summary>
        public Uri PictureUrl { get; set; }

        /// <summary>
        /// A picture of the artist, if one exists.
        /// </summary>
        public Picture Picture { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}