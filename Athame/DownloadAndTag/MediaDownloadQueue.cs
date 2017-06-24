using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Athame.Logging;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;
using Athame.Utils;

namespace Athame.DownloadAndTag
{
    public class MediaDownloadQueue : List<EnqueuedCollection>
    {
        private const string Tag = nameof(MediaDownloadQueue);

        public CancellationTokenSource CancellationTokenSource { get; set; }

        private readonly bool useTempFile;

        public MediaDownloadQueue(bool useTempFile)
        {
            this.useTempFile = useTempFile;
        }

        public EnqueuedCollection Enqueue(MusicService service, IMediaCollection collection, string pathFormat)
        {
            var item = new EnqueuedCollection
            {
                Service = service,
                PathFormat = pathFormat,
                MediaCollection = collection
            };
            Add(item);
            return item;
        }

        #region Events
        /// <summary>
        /// Raised before a media collection is downloaded.
        /// </summary>
        public event EventHandler<CollectionDownloadEventArgs> CollectionDequeued;

        protected void OnCollectionDequeued(CollectionDownloadEventArgs e)
        {
            CollectionDequeued?.Invoke(this, e);
        }

        /// <summary>
        /// Raised before a track is downloaded.
        /// </summary>
        public event EventHandler<TrackDownloadEventArgs> TrackDequeued;

        protected void OnTrackDequeued(TrackDownloadEventArgs e)
        {
            TrackDequeued?.Invoke(this, e);
        }

        /// <summary>
        /// Raised after a track is downloaded.
        /// </summary>
        public event EventHandler<TrackDownloadEventArgs> TrackDownloadCompleted;

        protected void OnTrackDownloadCompleted(TrackDownloadEventArgs e)
        {

            TrackDownloadCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// Raised when a track's download progress changes.
        /// </summary>
        public event EventHandler<TrackDownloadEventArgs> TrackDownloadProgress;

        protected void OnTrackDownloadProgress(TrackDownloadEventArgs e)
        {
            TrackDownloadProgress?.Invoke(this, e);
        }

        public event EventHandler<ExceptionEventArgs> Exception;

        protected void OnException(ExceptionEventArgs e)
        {
            Exception?.Invoke(this, e);

        }
        #endregion

        private ExceptionSkip skip;

        public async Task StartDownloadAsync()
        {
            var queueView = new Queue<EnqueuedCollection>(this);
            while (queueView.Count > 0)
            {
                var currentItem = queueView.Dequeue();
                OnCollectionDequeued(new CollectionDownloadEventArgs
                {
                    Collection = currentItem,
                    CurrentCollectionIndex = (Count - queueView.Count) - 1,
                    TotalNumberOfCollections = Count
                });
                if (await DownloadCollectionAsync(currentItem)) continue;
                if (skip == ExceptionSkip.Fail)
                {
                    return;
                }
            }
        }

        public EnqueuedCollection ItemById(string id)
        {
            return (from item in this
                    where item.MediaCollection.Id == id
                    select item).FirstOrDefault();

        }

        private void EnsureParentDirectories(string path)
        {
            var parentPath = Path.GetDirectoryName(path);
            if (parentPath == null) return;
            Directory.CreateDirectory(parentPath);
        }

        private async Task<bool> DownloadCollectionAsync(EnqueuedCollection collection)
        {
            var tracksCollectionLength = collection.MediaCollection.Tracks.Count;
            var tracksQueue = new Queue<Track>(collection.MediaCollection.Tracks);
            while (tracksQueue.Count > 0)
            {
                var currentItem = tracksQueue.Dequeue();
                var eventArgs = new TrackDownloadEventArgs
                {
                    CurrentItemIndex = (tracksCollectionLength - tracksQueue.Count) - 1,
                    PercentCompleted = 0M,
                    State = DownloadState.PreProcess,
                    TotalItems = tracksCollectionLength,
                    TrackFile = null
                };
                OnTrackDequeued(eventArgs);

                try
                {
                    if (!currentItem.IsDownloadable)
                    {
                        continue;
                    }
                    OnTrackDownloadProgress(eventArgs);
                    if (currentItem.Album?.CoverPicture != null)
                    {
                        // Download album artwork if it's not cached
                        var albumSmid = currentItem.Album.GetSmid(collection.Service.Info.Name).ToString();
                        if (!ImageCache.Instance.HasItem(albumSmid))
                        {
                            eventArgs.State = DownloadState.DownloadingAlbumArtwork;
                            OnTrackDownloadProgress(eventArgs);
                            try
                            {
                                await ImageCache.Instance.AddByDownload(albumSmid, currentItem.Album.CoverPicture);
                            }
                            catch (Exception ex)
                            {
                                ImageCache.Instance.AddNull(albumSmid);
                                Log.WriteException(Level.Warning, Tag, ex, "Exception occurred when download album artwork:");
                            }
                        }
                    }
                    eventArgs.TrackFile = await collection.Service.GetDownloadableTrackAsync(currentItem);
                    var downloader = collection.Service.GetDownloader(eventArgs.TrackFile);
                    downloader.Progress += (sender, args) =>
                    {
                        eventArgs.Update(args);
                        OnTrackDownloadProgress(eventArgs);
                    };
                    downloader.Done += (sender, args) =>
                    {
                        eventArgs.State = DownloadState.PostProcess;
                        OnTrackDownloadProgress(eventArgs);
                    };
                    var path = eventArgs.TrackFile.GetPath(collection.PathFormat);
                    var tempPath = path;
                    if (useTempFile) tempPath += "-temp";
                    EnsureParentDirectories(tempPath);
                    eventArgs.State = DownloadState.Downloading;
                    await downloader.DownloadAsyncTask(eventArgs.TrackFile, tempPath);

                    // Attempt to dispose the downloader, since the most probable case will be that it will
                    // implement IDisposable if it uses sockets
                    var disposableDownloader = downloader as IDisposable;
                    disposableDownloader?.Dispose();

                    // Write the tag
                    eventArgs.State = DownloadState.WritingTags;
                    OnTrackDownloadProgress(eventArgs);
                    TrackTagger.Write(collection.Service.Info.Name, currentItem, eventArgs.TrackFile, 
                        Program.DefaultSettings.Settings.AlbumArtworkSaveFormat, tempPath);

                    // Rename to proper path
                    if (useTempFile)
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.Move(tempPath, path);
                    }


                }
                catch (Exception ex)
                {
                    var exEventArgs = new ExceptionEventArgs { CurrentState = eventArgs, Exception = ex };
                    OnException(exEventArgs);
                    switch (exEventArgs.SkipTo)
                    {
                        case ExceptionSkip.Item:
                            continue;

                        case ExceptionSkip.Collection:
                        case ExceptionSkip.Fail:
                            skip = exEventArgs.SkipTo;
                            return false;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                // Raise the completed event even if an error occurred
                OnTrackDownloadCompleted(eventArgs);

            }
            return true;
        }
    }
}
