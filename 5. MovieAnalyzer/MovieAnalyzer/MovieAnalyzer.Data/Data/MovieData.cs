using MovieAnalyzer.Data.Model;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAnalyzer.Data
{
    public class MovieData : EntityData<Movie>
    {
        public Movie GetByPK(Movie movies)
        {
            using (var context = DbContextFactory.Create<MovieAnalyzerEntities>())
            {
                return context.Movies.FirstOrDefault(x => x.MovieId == movies.MovieId && x.Date == movies.Date);
            }
        }

        public Movie GetByPK(int movieId, DateTime date) => GetByPK(new Movie(movieId, date));

        public void DeleteByPK(Movie movie)
        {
            using (var context = DbContextFactory.Create<MovieAnalyzerEntities>())
            {
                var toDeleteMovie = GetByPK(movie);

                if (toDeleteMovie == null)
                    return;

                context.Entry(toDeleteMovie).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteByPK(int movieId, DateTime date) => DeleteByPK(new Movie(movieId, date));

        public List<int> GetOnlyById()
        {
            using (var context = DbContextFactory.Create<MovieAnalyzerEntities>())
            {
                var movieIds = from x in context.Movies
                               select x.MovieId;

                return movieIds.Distinct().ToList();
            }
        }

        public long GetAccumulatedSales(int movieId)
        {
            using (var context = DbContextFactory.Create<MovieAnalyzerEntities>())
            {
                var movies = from x in context.Movies
                             where x.MovieId == movieId
                             select x;

                return movies.Select(x => x.AccumulatedSales).Sum();
            }
        }

        public string GetMovieNames(int movieId)
        {
            using (var context = DbContextFactory.Create<MovieAnalyzerEntities>())
            {
                var movieNames = from x in context.Movies
                                 where x.MovieId == movieId
                                 select x.Name;

                return movieNames.FirstOrDefault();
            }
        }

        public List<MovieModel> GetModels()
        {
            List<int> movieIds = DataRepository.Movie.GetOnlyById();

            Dictionary<string, long> movieSalesTotal = new Dictionary<string, long>();

            for (int i = 0; i < movieIds.Count; i++)
            {
                var movieNames = DataRepository.Movie.GetMovieNames(movieIds[i]);
                var movieSales = DataRepository.Movie.GetAccumulatedSales(movieIds[i]);
                movieSalesTotal.Add(movieNames, movieSales);
            }

            var query = from d in movieSalesTotal
                        orderby d.Value
                        select new MovieModel {
                            MovieTitles = d.Key,
                            TotalSum = d.Value
                        };

            return query.ToList();
        }
    }
}
