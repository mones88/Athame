using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void ResultsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<IMediaCollection>
            {
                MockDataGen.GenerateAlbumWithTracks(),
                MockDataGen.GeneratePlaylist()
            };
            ResultsListBox.ItemsSource = items;
        }
    }
}
