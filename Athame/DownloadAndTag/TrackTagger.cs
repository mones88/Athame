using System;
using System.IO;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;
using Athame.Settings;
using TagLib;
using File = TagLib.File;
using SysFile = System.IO.File;

namespace Athame.DownloadAndTag
{
    public class TrackTagger
    {
        private const string CopyrightText = "Respect the artists! Pay for music when you can! Downloaded with Athame";

        public static void Write(FileType fileType, string path, Track track)
        {
            AlbumArtFile artworkFile = null;
            var url = track.Album.CoverUri.ToString();
            if (AlbumArtCache.Instance.HasItem(url) && AlbumArtCache.Instance.Get(url) != null)
            {
                artworkFile = AlbumArtCache.Instance.Get(track.Album.CoverUri.ToString());
            }

            using (var file = File.Create(new File.LocalFileAbstraction(path), fileType.MimeType, ReadStyle.Average))
            {
                file.Tag.Title = track.Title;
                file.Tag.Performers = new[] {track.Artist.Name};
                if (track.Album.Artist != null)
                {
                    file.Tag.AlbumArtists = new[] {track.Album.Artist.Name};
                }
                file.Tag.Genres = new[] {track.Genre};
                file.Tag.Album = track.Album.Title;
                file.Tag.Track = (uint) track.TrackNumber;
                file.Tag.TrackCount = (uint) (track.Album.GetNumberOfTracksOnDisc(track.DiscNumber) ?? 0);
                file.Tag.Disc = (uint) track.DiscNumber;
                file.Tag.DiscCount = (uint) (track.Album.GetTotalDiscs() ?? 0);
                file.Tag.Year = (uint) track.Year;
                file.Tag.Copyright = CopyrightText;
                file.Tag.Comment = CopyrightText;
                if (artworkFile != null)
                {
                    file.Tag.Pictures = new IPicture[] {new Picture(new ByteVector(artworkFile.Data))};
                }

                file.Save();
            }
            

            string fileName = null;
            switch (Program.DefaultSettings.Settings.AlbumArtworkSaveFormat)
            {
                case AlbumArtworkSaveFormat.DontSave:
                    break;
                case AlbumArtworkSaveFormat.AsCover:
                    fileName = artworkFile?.FileType.Append("cover");
                    break;
                case AlbumArtworkSaveFormat.AsArtistAlbum:
                    fileName = artworkFile?.FileType.Append($"{track.Artist} - {track.Album.Title}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (fileName != null && artworkFile != null)
            {
                var parentDir = Path.GetDirectoryName(path);
                SysFile.WriteAllBytes(Path.Combine(parentDir, fileName), artworkFile.Data);
            }
        }
    }
}
