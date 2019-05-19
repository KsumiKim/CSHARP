using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step4.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ChinookEntities context = new ChinookEntities();

            context.Database.Log = PrintLog;

            Album firstAlbum = context.Albums.First();
            firstAlbum.Title = DateTime.Now.ToString();

            context.SaveChanges();

            Album newAlbum = new Album();
            newAlbum.Title = DateTime.Now.ToString();
            newAlbum.ArtistId = 1;

            context.Albums.Add(newAlbum);
            context.SaveChanges();

//            return;
            

            //List<Album> albums = 
            //    context.Albums
            //    .Where(x => x.Title.Contains("rock"))
            //    .ToList();

            string name = "beatles";
            string title = "help!";

            int skip = 10;
            int top = 5;


            // 지연된 실행(평가) - lazy evaluation

            var query = from x in context.Tracks
                        select x;

            if (name != "")
                query = query.Where(x => x.Album.Artist.Name.Contains(name));

            if (title != "")
                query = query.Where(x => x.Album.Title.Contains(title));

            query = query.Skip(skip).Take(top);


            var tracks = query.ToList();

            foreach (var track in tracks)
            {
                Console.WriteLine($"{track.Name}");
            }
        }
        private static void PrintLog(string log)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine(log);
            Console.WriteLine("---------------------");
        }
    }
}
