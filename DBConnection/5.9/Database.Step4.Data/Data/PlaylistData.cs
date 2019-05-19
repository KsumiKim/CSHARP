using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step4.Data
{
    public class PlaylistData
    {
        private static ChinookEntities CreateContext()
        {
            var context = new ChinookEntities();
            context.Database.Log = PrintToConsoleWindow;

            return context;
        }

        private static void PrintToConsoleWindow(string log)
        {
            Console.WriteLine(log);

        }

        public int GetCount()
        {
            using (var context = CreateContext())
            {
                return context.Playlists.Count();
            }

            throw new NotImplementedException();
        }

        public List<Playlist> GetAll()
        {
            using (var context = CreateContext())
            {
                return context.Playlists.ToList();
            }

            throw new NotImplementedException();
        }

        public Playlist GetByPK(int playlistId)
        {
            using (var context = CreateContext())
            {
                return context.Playlists.FirstOrDefault(x => x.PlaylistId == playlistId);
            }

            throw new NotImplementedException();
        }

        public static void Insert(Playlist playlist)
        {
            using (var context = CreateContext())
            {
                context.Entry(playlist).State = EntityState.Added;

                context.SaveChanges();
            }

            throw new NotImplementedException();
        }

        public string Update(Playlist playlist)
        {
            using (var context = CreateContext())
            {
                context.Entry(playlist).State = EntityState.Modified;

                context.SaveChanges();
            }

            throw new NotImplementedException();
        }

        public string Delete(Playlist playlist)
        {
            using (var context = CreateContext())
            {
                context.Entry(playlist).State = EntityState.Deleted;

                context.SaveChanges();
            }

            throw new NotImplementedException();
        }
    }
}
