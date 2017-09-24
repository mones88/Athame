using System;
using System.IO;
using Athame;
using Athame.PluginAPI.Downloader;
using Athame.PluginAPI.Service;
using AthameWPF.Caching;
using AthameWPF.Downloader;
using AthameWPF.Settings;
using TagLib;
using File = TagLib.File;
using SysFile = System.IO.File;

namespace AthameWPF.Tagging
{
    public class TrackTagger
    {
        private const string CopyrightText = "Respect the artists! Pay for music when you can! Downloaded with Athame";

        private static void WriteArtworkFile(string directory, AlbumArtworkSaveFormat saveFormat, Track track, PictureCacheEntry albumArtwork)
        {
            string fileName = null;
            switch (saveFormat)
            {
                case AlbumArtworkSaveFormat.DontSave:
                    break;
                case AlbumArtworkSaveFormat.AsCover:
                    fileName = albumArtwork?.OriginalPicture.FileType.Append("cover");
                    break;
                case AlbumArtworkSaveFormat.AsArtistAlbum:
                    fileName = albumArtwork?.OriginalPicture.FileType.Append($"{track.Artist} - {track.Album.Title}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (fileName == null) return;
            SysFile.WriteAllBytes(Path.Combine(directory, fileName), albumArtwork.FullSize);
        }

        public static void Write(EnqueuedTrack completedTrack, AlbumArtworkSaveFormat saveFormat, string path)
        {
            // Get album artwork from cache
            PictureCacheEntry albumArtwork = null;
            if (PictureCache.HasPicture(completedTrack.Smid.ToString()))
            {
                albumArtwork = PictureCache.GetPicture(completedTrack.Smid.ToString());
            }

            // Write track tags
            var track = completedTrack.OriginalTrack;
            using (var file = File.Create(new File.LocalFileAbstraction(path), 
                completedTrack.TrackFile.FileType.MimeType, ReadStyle.Average))
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
                if (albumArtwork != null)
                {
                    file.Tag.Pictures = new IPicture[] { new TagLib.Picture(new ByteVector(albumArtwork.FullSize)) };
                }

                file.Save();
            }

            // Write album artwork to file if requested
            string parentDirectory;
            if (saveFormat != AlbumArtworkSaveFormat.DontSave && 
                (parentDirectory = Path.GetDirectoryName(path)) != null)
            {
                WriteArtworkFile(parentDirectory, saveFormat, track, albumArtwork);
            }
           
        }
    }
}
