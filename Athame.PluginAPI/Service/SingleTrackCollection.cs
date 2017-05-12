using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a single track for use in scenarios where a <see cref="IMediaCollection"/> is required.
    /// This does not need to be handled by your service implementation, and is meant for internal use only.
    /// </summary>
    public class SingleTrackCollection : IMediaCollection
    {
        /// <summary>
        /// Constructs a new instance based on a single track. Also sets properties of <see cref="IMediaCollection"/>
        /// to this track's equivalent properties.
        /// </summary>
        /// <param name="t">The single track to use.</param>
        internal SingleTrackCollection(Track t)
        {
            Tracks = new List<Track>(1) { t };
            Title = t.Title;
            Id = t.Id;
            CustomMetadata = t.CustomMetadata;
            Duration = t.Duration;
        }

        public IList<Track> Tracks { get; set; }
        /// <summary>
        /// The only track in this collection.
        /// </summary>
        public Track Track => Tracks[0];
        public string Title { get; set; }
        public string Id { get; set; }
        public IReadOnlyCollection<Metadata> CustomMetadata { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
