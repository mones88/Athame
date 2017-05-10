using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a link to a URL that is relevant to the sign-in process.
    /// </summary>
    public class SignInLink
    {
        /// <summary>
        /// The text to display for this link.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The link's location.
        /// </summary>
        public Uri Link { get; set; }
    }
}
