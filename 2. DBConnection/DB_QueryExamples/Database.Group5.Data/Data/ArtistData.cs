using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Database.Group5.Data
{
    public class ArtistData
    {
        
        private static ChinookEntities CreateContext()
        {
            ChinookEntities context = new ChinookEntities();
            return context;
        }
        public void Insert(Artist artist)
        {
            using (var c = CreateContext())
            {
                c.Entry(artist).State = EntityState.Added;
                c.SaveChanges();
            }
        }
        public List<Artist> GetAll()
        {
            using (var c = CreateContext())
            {
                var query = from s in c.Artists
                            select s;
                return query.ToList();
            }
        }
        public Artist GetByPK(int artistId)
        {
            using (var c = CreateContext())
            {
                return c.Artists.FirstOrDefault(x => x.ArtistId == artistId);
            }
        }
        public void Update(Artist artist)
        {
            using (var c = CreateContext())
            {
                c.Entry(artist).State = EntityState.Modified;
                c.SaveChanges();
            }
        }

        public int GetPlaylistTotalByArtist(string keyword)
        {
            using (var context = CreateContext())
            {
                var albumQuery = from x in context.Albums
                                 where x.Artist.Name.Contains(keyword)
                                 select x.AlbumId;

                List<int> albumdIds = albumQuery.ToList();

                var trackQuery = from t in context.Tracks
                                 where albumdIds.Contains(t.AlbumId.Value)
                                 select t;

                return trackQuery.Count();
            }
        }
    }
}
