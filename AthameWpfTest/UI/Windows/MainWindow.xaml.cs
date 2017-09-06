using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using AthameWPF.UI.Pages;

namespace AthameWPF.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double ScreenSizeFillPercent = 0.6;

        private readonly SearchPage SearchPage = new SearchPage();
        private readonly QueuePage QueuePage = new QueuePage();
        private readonly SettingsPage SettingsPage = new SettingsPage();

        private readonly Page DefaultPage;


        public MainWindow()
        {
            InitializeComponent();
            var workArea = SystemParameters.WorkArea;
            Width = (workArea.Width * ScreenSizeFillPercent);
            Height = (workArea.Height * ScreenSizeFillPercent);
            DefaultPage = SearchPage;
        }

        #region ' Main Switcher Anims '
        

        #endregion

        private void MainSwitcherSettingsButton_OnChecked(object sender, RoutedEventArgs e)
        {
            MainContentFrame?.Navigate(SettingsPage);
        }

        private void MainSwitcherQueueButton_OnChecked(object sender, RoutedEventArgs e)
        {
            MainContentFrame?.Navigate(QueuePage);
        }

        private void MainSwitcherSearchButton_OnChecked(object sender, RoutedEventArgs e)
        {
            MainContentFrame?.Navigate(SearchPage);
        }

        private void MainContentFrame_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(DefaultPage);
        }
    }
}
