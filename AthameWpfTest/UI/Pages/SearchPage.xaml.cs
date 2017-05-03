using System.Windows;
using System.Windows.Controls;

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

        private void ByUrlSecMenuOption_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TempAlbum_OnLoaded(object sender, RoutedEventArgs e)
        {
            TempAlbum.Album = MockDataGen.GenerateAlbum();
        }
    }
}
