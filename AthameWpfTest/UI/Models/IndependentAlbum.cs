using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Models
{
    public class IndependentAlbum
    {
        public MusicService Service { get; set; }
        public IndependentAlbum(MusicService service, Album album)
        {
            Service = service;
            Album = album;
            Smid = new ServiceMediaId(Service.Info.Name, album);
        }

        public Album Album { get; set; }
        public ServiceMediaId Smid { get; }
    }
}
