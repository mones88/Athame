using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI
{
    public class MediaTypeGroupDescription : GroupDescription
    {

        public override object GroupNameFromItem(object item, int level, CultureInfo culture)
        {
            if (item is Album)
            {
                return "Albums";
            }
            if (item is Playlist)
            {
                return "Playlists";
            }
            if (item is SingleTrackCollection || item is Track)
            {
                return "Tracks";
            }
            if (item is Artist)
            {
                return "Artists";
            }
            return item.GetType().Name;
        }
    }
}
