using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.PluginAPI.Service;

namespace AthameWPF
{
    public struct Limit
    {
        private Random random;

        public Limit(Random rng, int lower, int upper)
        {
            Lower = lower;
            Upper = upper;
            random = rng;
        }

        public int Lower { get; set; }
        public int Upper { get; set; }

        public int RandomValue(bool lowerInclusive = true, bool upperInclusive = true)
        {
            return random.Next(lowerInclusive ? Lower : Lower - 1, upperInclusive ? Upper : Upper - 1);
        }

        public IList<T> RandomSet<T>(IList<T> array, bool lowerInclusive = true, bool upperInclusive = true)
        {
            var value = RandomValue(lowerInclusive, upperInclusive);
            var list = new List<T>(value);
            for (var i = 0; i < value; i++)
            {
                list.Add(array[random.Next(array.Count)]);
            }
            return list;
        }
    }

    public static class MockDataGen
    {
        private static readonly string[] ArtistNames = {
            "Jerilyn Liles", "Keena Ardrey", "Zena Vandyne", "Merlene Tam", "Marine Trogdon", "Franklin Kaminski",
            "Lawrence Parise", "Katrice Lindsley", "Marylou Shaul", "Marco Morrisette", "Amal Roux", "Yuki Mattinson",
            "Dimple Sholar", "Ayana Lichty", "Abbey Stalvey", "Anastasia Faucette", "Shanti Lady", "Hulda Overall",
            "Paris Westbrook", "Christiane Vazguez"
        };

        private static readonly string[] AlbumWords =
        {
            "hungry", "mailbox", "dazzling", "stage", "basin", "noise", "fancy", "labored", "scream", "whirl",
            "literate", "account"
        };

        private static readonly string[] TrackWords =
        {
            "rainstorm", "wistful", "elbow", "warm", "dinner", "powder",
            "fold", "sun", "brief", "cactus", "hang", "snobbish", "curve", "trashy", "grubby", "weight", "influence",
            "print", "design", "plausible", "word", "cap", "righteous", "efficient", "incandescent", "top", "tasty",
            "bare", "domineering", "hands"
        };

        private static Random rng = new Random();

        public static Artist GenerateArtist()
        {
            var artistName = ArtistNames[rng.Next(ArtistNames.Length)];
            return new Artist { Id = Guid.NewGuid().ToString(), Name = artistName };
        }

        public static Album GenerateAlbum()
        {
            var albumYear = new Limit(rng, 1980, DateTime.Now.Year);
            var artist = GenerateArtist();

            return new Album
            {
                Artist = artist,
                Id = Guid.NewGuid().ToString(),
                Title = GenerateTitle(),
                Year = albumYear.RandomValue(),
                CoverUri = new Uri("https://placehold.it/256")
            };
        }

        public static string GenerateTitle()
        {
            var wordsInAlbumTitle = new Limit(rng, 1, 3);
            return ToTitleCase(String.Join(" ", wordsInAlbumTitle.RandomSet(AlbumWords)));
        }

        public static Album GenerateAlbumWithTracks()
        {
            var album = GenerateAlbum();
            var tracksPerAlbum = new Limit(rng, 8, 20);
            var trackCount = tracksPerAlbum.RandomValue();
            album.Tracks = GenerateTracks(album, album.Artist).Take(trackCount).ToList();

            return album;
        }

        public static IEnumerable<Track> GenerateTracks(Album album = null, Artist artist = null)
        {
            var wordsInTrackTitle = new Limit(rng, 1, 6);
            var counter = 0;
            var newAlbum = album ?? GenerateAlbum();
            var newArtist = artist ?? GenerateArtist();
            while (true)
            {
                yield return new Track
                {
                    Album = newAlbum,
                    Artist = newArtist,
                    DiscNumber = 1,
                    TrackNumber = ++counter,
                    Id = Guid.NewGuid().ToString(),
                    Title = ToTitleCase(String.Join(" ", wordsInTrackTitle.RandomSet(TrackWords)))
                };
            }
        }

        public static Playlist GeneratePlaylist()
        {
            var trackCount = new Limit(rng, 4, 60).RandomValue();
            return new Playlist
            {
                Id = Guid.NewGuid().ToString(),
                Title = GenerateTitle(),
                Tracks = GenerateTracks().Take(trackCount).ToArray()
            };
        }

        private static Playlist designerPlaylist;

        public static Playlist DesignerPlaylist => designerPlaylist ?? (designerPlaylist = GeneratePlaylist());

        private static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
