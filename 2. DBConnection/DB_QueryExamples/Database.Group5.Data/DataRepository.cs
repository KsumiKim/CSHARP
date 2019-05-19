using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Group5.Data
{
    public class DataRepository
    {
        static DataRepository()
        {
            Track = new TrackData();
            Playlist = new PlaylistData();
            Genre = new GenreData();
            Artist = new ArtistData();
            Album = new AlbumData();
        }

        public static TrackData Track { get; }
        public static PlaylistData Playlist { get; }
        public static GenreData Genre { get; }
        public static ArtistData Artist { get; set; }
        public static AlbumData Album { get; private set; }
    }
}
