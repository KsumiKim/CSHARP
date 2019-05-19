using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step4.Data
{
    public class DataRepository
    {
        static DataRepository()
        {
            Playlist = new PlaylistData();
        }

        private static AlbumData _album = new AlbumData();

        public static AlbumData Album
        {
            get
            {
                return _album;
            }
        }

        public static PlaylistData Playlist { get; }
    }
}
