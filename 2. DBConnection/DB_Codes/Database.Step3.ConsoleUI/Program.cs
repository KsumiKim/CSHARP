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
            AlbumData albumData = new AlbumData();
            List<Album> albums = albumData.Search(10, "b");

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
