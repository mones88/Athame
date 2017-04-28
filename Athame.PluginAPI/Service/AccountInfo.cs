using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Provides basic information about a signed in user account.
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// The user's name. This could be a screen name or first name/last name. May be null.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The user's unique ID or email address.
        /// </summary>
        public string DisplayId { get; set; }
    }
}
