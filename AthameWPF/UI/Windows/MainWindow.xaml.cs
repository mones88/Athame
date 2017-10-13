using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using AthameWPF.UI.Controls;

namespace AthameWPF.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double ScreenSizeFillPercent = 0.6;

        public MainWindow()
        {
            InitializeComponent();
            var workArea = SystemParameters.WorkArea;
            Width = (workArea.Width * ScreenSizeFillPercent);
            Height = (workArea.Height * ScreenSizeFillPercent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}