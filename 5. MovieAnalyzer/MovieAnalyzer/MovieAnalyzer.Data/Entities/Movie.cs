using System;
using System.Collections.Generic;

namespace MovieAnalyzer.Data
{
    partial class Movie
    {
        public Movie(int movieId, string date) : this(movieId, StringToDate(date))
        {
        }

        public override string ToString()
        {
            return Name;
        }

        public Movie(int movieId, DateTime date, DateTime releaseDate = default, string name = "Arrival", int rank = 2, long accumulatedSales = 2000, int accumulatedAudience = 2000, int noOfScreens = 5)
        {
            if (releaseDate == default)
                releaseDate = DateTime.Now;

            MovieId = movieId;

            if (date == default)
                date = DateTime.Now;
            Name = name;
            ReleaseDate = releaseDate;
            AccumulatedSales = accumulatedSales;
            AccumulatedAudience = accumulatedAudience;
            NoOfScreens = noOfScreens;
            Date = date;
        }

        public Movie()
        {
        }

        //raw format : 20190715
        private static DateTime StringToDate(string rawDate) => new DateTime(int.Parse(rawDate.Substring(0, 4)), int.Parse(rawDate.Substring(4, 2)), int.Parse(rawDate.Substring(6, 2)));
    }
}
