using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Downloader;

namespace Athame.DownloadAndTag
{
    public class ImageCache : IDisposable
    {
        public static readonly ImageCache Instance = new ImageCache();

        private readonly Dictionary<string, ImageFile> items = new Dictionary<string, ImageFile>();
        private readonly WebClient mClient = new WebClient();

        public async Task AddByDownload(string url)
        {
            var data = await mClient.DownloadDataTaskAsync(url);
            var contentMimeType = mClient.ResponseHeaders[HttpResponseHeader.ContentType];

            items.Add(url, new ImageFile
            {
                Data = data,
                DownloadUri = new Uri(url),
                FileType = MediaFileTypes.ByMimeType(contentMimeType)
            });
        }

        public void Add(ImageFile file)
        {
            items[file.DownloadUri.ToString()] = file;
        }

        public void AddNull(string url)
        {
            items[url] = null;
        }

        private const string DefaultKey = "_default";
        public ImageFile GetDefault()
        {
            return items.ContainsKey(DefaultKey) ? items[DefaultKey] : null;
        }

        public void SetDefault(ImageFile file)
        {
            items[DefaultKey] = file;
        }

        public bool HasItem(string url)
        {
            return items.ContainsKey(url);
        }

        public ImageFile Get(string url)
        {
            return items[url];
        }

        public void WriteToFile(string url, string filePath)
        {
            var item = items[url];
            File.WriteAllBytes(item.FileType.Append(filePath), item.Data);
        }

        public void Dispose()
        {
            mClient?.Dispose();
        }
    }
}
