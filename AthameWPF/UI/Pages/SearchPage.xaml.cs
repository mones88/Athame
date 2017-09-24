using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {

        private readonly List<object> items;
        public SearchPage()
        {
            InitializeComponent();
            items = new List<object>
            {
                MockDataGen.GenerateArtist(),
                MockDataGen.GenerateArtist(),
                MockDataGen.GenerateArtist(),
                MockDataGen.GenerateAlbum(),
                MockDataGen.GenerateAlbum(),
                MockDataGen.GenerateAlbum(),
                MockDataGen.GeneratePlaylist(),
                MockDataGen.GeneratePlaylist(),
                MockDataGen.GeneratePlaylist()

            };
            items.AddRange(MockDataGen.GenerateTracks().Take(3));
            ResultsListBox.ItemsSource = items;
            var view = (CollectionView)CollectionViewSource.GetDefaultView(ResultsListBox.ItemsSource);
            var groupDescription = new MediaTypeGroupDescription();
            view?.GroupDescriptions?.Add(groupDescription);
        }

        private void ResultsListBox_Loaded(object sender, RoutedEventArgs e)
        {

            

        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;
            var item = e.AddedItems[0];
            var album = item as Album;
            if (album != null)
            {
                NavigationService?.Navigate(new ViewMediaPage($"search results for \"{PrimaryInputTextBox.Text}\"", album));
            }
        }
    }
}
