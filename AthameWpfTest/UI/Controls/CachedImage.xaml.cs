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
using AthameWPF.Caching;

namespace AthameWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for CachedImage.xaml
    /// </summary>
    public partial class CachedImage
    {
        private const double ThumbnailSize = 100;

        public CachedImage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MediaIdProperty = DependencyProperty.Register(
            "MediaId", typeof(string), typeof(CachedImage), new PropertyMetadata(default(string), MediaIdPropertyChanged));

        private static async void MediaIdPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var _this = (CachedImage)dependencyObject;
            var imageControl = _this.MainImage;
            var _value = (string) dependencyPropertyChangedEventArgs.NewValue;
            if (PictureCache.HasPicture(_value))
            {
                var entry = PictureCache.GetPicture(_value);
                if (_this.RenderSize.Width > ThumbnailSize || _this.RenderSize.Height > ThumbnailSize)
                {
                    await entry.GetFullSizeAsync();
                    imageControl.Source = entry.XamlFullSizeBitmap;
                }
                else
                {
                    await entry.GetThumbnailAsync();
                    imageControl.Source = entry.XamlThumbnailBitmap;
                }
            }
            else
            {
                // TODO: write to log when I figure this shit out
            }
        }

        public string MediaId
        {
            get { return (string) GetValue(MediaIdProperty); }
            set { SetValue(MediaIdProperty, value); }
        }

        
    }
}
