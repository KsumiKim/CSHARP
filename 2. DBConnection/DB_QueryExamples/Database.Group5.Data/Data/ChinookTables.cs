using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Group5.Data
{

        public enum ChinookTables { Album, Artist, Track };
        public enum AlbumColumns { AlbumId, Title, ArtistId };
        public enum ArtistColumns { ArtistId, Name };
        public enum TrackColumns { TrackId, Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice };

}
