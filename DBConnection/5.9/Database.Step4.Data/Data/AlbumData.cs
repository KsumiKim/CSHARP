using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step4.Data
{
    public class AlbumData
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

        public static int GetCount()
        {
            using (var context = CreateContext())
            {
                return context.Albums.Count();
            }

            throw new NotImplementedException();
        }

        public static List<Album> GetAll()
        {
            using (var context = CreateContext())
            {
                return context.Albums.ToList();
            }

            throw new NotImplementedException();
        }

        public Album GetByPK(int albumId)
        {
            using (var context = CreateContext())
            {
                return context.Albums.FirstOrDefault(x => x.AlbumId == albumId);
            }

            throw new NotImplementedException();
        }

        public static void Insert(Album album)
        {
            using (var context = CreateContext())
            {
                context.Entry(album).State = EntityState.Added;

                context.SaveChanges();
            }

            //throw new NotImplementedException();
        }

        public static string Update(Album album)
        {
            using (var context = CreateContext())
            {
                context.Entry(album).State = EntityState.Modified;

                context.SaveChanges();
            }

            throw new NotImplementedException();
        }

        public static string Delete(Album album)
        {
            using (var context = CreateContext())
            {
                context.Entry(album).State = EntityState.Deleted;

                context.SaveChanges();
            }

            throw new NotImplementedException();
        }
    }
}
