using System.Windows;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView
    {
        public AlbumView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AlbumProperty = DependencyProperty.Register(
            "Album", typeof(Album), typeof(AlbumView), new PropertyMetadata(default(Album)));

        public Album Album
        {
            get { return (Album) GetValue(AlbumProperty); }
            set { SetValue(AlbumProperty, value); }
        }
    }
}
