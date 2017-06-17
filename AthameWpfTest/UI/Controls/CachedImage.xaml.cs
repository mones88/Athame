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
using AthameWPF.Logging;
using AthameWPF.Utils;

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
            var _this = (CachedImage) dependencyObject;
            var _value = (string) dependencyPropertyChangedEventArgs.NewValue;
            if (PictureCache.HasPicture(_value))
            {
                var entry = PictureCache.GetPicture(_value);
                if (_this.RenderSize.Width > ThumbnailSize || _this.RenderSize.Height > ThumbnailSize)
                {
                    await entry.GetFullSizeAsync();
                    _this.MainImage.Source = entry.XamlFullSizeBitmap;
                }
                else
                {
                    if (entry.OriginalPicture.IsThumbnailAvailable)
                    {
                        await entry.GetThumbnailAsync();
                        _this.MainImage.Source = entry.XamlThumbnailBitmap;
                    }
                    else
                    {
                        await entry.GetFullSizeAsync();
                        _this.MainImage.Source = entry.XamlFullSizeBitmap;
                    }
                }
            }
            else
            {
                _this.MainImage.Source = new BitmapImage(ResourceUriHelper.BuildUri("DefaultArtwork.png"));
                Log.Warning(nameof(CachedImage), $"No picture cache entry for {_value}");
            }
        }

        public string MediaId
        {
            get { return (string) GetValue(MediaIdProperty); }
            set { SetValue(MediaIdProperty, value); }
        }

        
    }
}
