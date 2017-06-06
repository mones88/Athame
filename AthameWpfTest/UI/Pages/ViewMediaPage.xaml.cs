using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Athame.PluginAPI.Service;

namespace AthameWPF.UI.Pages
{
    /// <summary>
    /// Interaction logic for CollectionViewPage.xaml
    /// </summary>
    public partial class ViewMediaPage : Page
    {
        private readonly string backString;

        public ViewMediaPage()
        {
            InitializeComponent();
        }

        public ViewMediaPage(string previousPageTitle, Album album)
        {
            InitializeComponent();
            backString = $"Back to {previousPageTitle}";
        }

        private void BackTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            BackTextBlock.Text = backString;
        }
    }
}
