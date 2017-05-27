using Athame.PluginAPI.Downloader;

namespace Athame.DownloadAndTag
{
    public class TrackDownloadEventArgs : DownloadEventArgs
    {
        public TrackFile TrackFile { get; set; }

        public decimal TotalProgress
        {
            get
            {
                if (TotalItems == 0)
                {
                    return 0;
                }
                return (CurrentItemIndex + PercentCompleted) / TotalItems;
            }
        }

        public int TotalItems { get; set; }

        public int CurrentItemIndex { get; set; }

        internal void Update(DownloadEventArgs eventArgs)
        {
            PercentCompleted = eventArgs.PercentCompleted;
            State = eventArgs.State;
        }
    }
}