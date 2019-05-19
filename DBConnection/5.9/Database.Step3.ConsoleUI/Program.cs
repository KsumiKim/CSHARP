using Database.Step3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step3.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Album a1 = DataRepository.Album.GetByPK(1);
            Playlist p1 = DataRepository.Playlist.GetByPK(1);
            Console.WriteLine(a1.Title);
            Console.WriteLine(p1.Name);

            Console.WriteLine(DataRepository.Album.GetAll().Count);
            Console.WriteLine(DataRepository.Playlist.GetAll().Count);
            return;


            Console.WriteLine(nameof(Playlist.Name));
            Console.WriteLine(nameof(Playlist.PlaylistId));


            List<Album> albums = DataRepository.Album.Search(10, "b");

            int count = 0;
            int sum = 0;

            foreach (Album album in albums)
            {
                count++;
                sum += album.AlbumId;
            }

            Console.WriteLine(sum / count);
        }
    }
}
