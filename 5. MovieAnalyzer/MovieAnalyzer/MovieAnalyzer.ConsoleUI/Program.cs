using MovieAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAnalyzer.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> movies = DataRepository.Movie.GetAll();

            Console.WriteLine(movies.FirstOrDefault());
        }
    }
}
