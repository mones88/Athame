using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;

namespace Athame.DownloadAndTag
{
    public class ImageCacheEntry
    {
        public Picture Picture { get; set; }
        public byte[] Data { get; set; }

    }
}

