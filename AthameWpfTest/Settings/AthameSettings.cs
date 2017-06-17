using System;
using System.Drawing;
using System.IO;

namespace AthameWPF.Settings
{
    public enum AlbumArtworkSaveFormat
    {
        DontSave,
        AsCover,
        AsArtistAlbum
    }

    public class MediaTypeSavePreference
    {
        public string SaveDirectory { get; set; }
        public string SaveFormat { get; set; }
        public bool AskForLocation { get; set; }

        public string GetPlatformSaveFormat()
        {
            return Path.DirectorySeparatorChar == '/' ? SaveFormat : SaveFormat.Replace('/', Path.DirectorySeparatorChar);
        }

        public MediaTypeSavePreference Clone()
        {
            return new MediaTypeSavePreference
            {
                AskForLocation = AskForLocation,
                SaveDirectory = SaveDirectory,
                SaveFormat = SaveFormat
            };
        }
    }

    public class WindowPreference
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
    }

    public class AthameSettings
    {
        // Defaults
        public AthameSettings()
        {
            AlbumArtworkSaveFormat = AlbumArtworkSaveFormat.DontSave;
            GeneralSavePreference = new MediaTypeSavePreference
            {
                AskForLocation = false,
                SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                SaveFormat = "{AlbumArtistOrArtist} - {Album.Title}/{TrackNumber} {Title}"
            };
            PlaylistSavePreference = new MediaTypeSavePreference
            {
                AskForLocation = true,
                SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                SaveFormat = "{AlbumArtistOrArtist} - {Title}"
            };
            PlaylistSavePreferenceUsesGeneral = false;
            MainWindowPreference = new WindowPreference();
        }

        public AlbumArtworkSaveFormat AlbumArtworkSaveFormat { get; set; }
        public MediaTypeSavePreference GeneralSavePreference { get; set; }
        public MediaTypeSavePreference PlaylistSavePreference { get; set; }
        public bool PlaylistSavePreferenceUsesGeneral { get; set; }
        public WindowPreference MainWindowPreference { get; set; }
    }
}
