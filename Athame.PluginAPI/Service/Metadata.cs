using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a custom metadata value. Custom metadata can be used to denote service-specific attributes,
    /// such as if a track is explicit or an album or playlist is available in a higher quality.
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// The metadata name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The metadata's value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Whether to display the metadata to the user or not.
        /// </summary>
        public bool CanDisplay { get; set; }
        /// <summary>
        /// If <see cref="CanDisplay"/> is true, will display as a boolean value depending on whether <see cref="Value"/>
        /// is the string "True" or "False", rather than the actual value. 
        /// </summary>
        public bool IsFlag { get; set; }

        /// <summary>
        /// Returns a clone of the current <see cref="Metadata"/> instance, with a new value.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>A new instance with the new value set.</returns>
        public Metadata Copy(string newValue)
        {
            return new Metadata
            {
                CanDisplay = CanDisplay,
                IsFlag = IsFlag,
                Name = Name,
                Value = newValue
            };
        }
    }
}
