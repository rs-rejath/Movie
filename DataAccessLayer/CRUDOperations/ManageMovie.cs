using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CRUDOperations
{
    public class ManageMovie
    {
        private readonly ApplicationDbContext _context;

        public ManageMovie(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to fetch all movie details (non-subscribed users)
        public List<MovieDetailModel> GetAllMovies()
        {
            var movies = _context.MovieDetails
                        .Select(m => new MovieDetailModel
                        {
                            Movie_Id = m.Movie_Id,
                            Movie_Name = m.Movie_Name,
                            Language = m.Language,
                            Genre = m.Genre,
                            Director = m.Director,
                            Cast = m.Cast,
                            Descriptiom = m.Descriptiom,
                            Image = m.Image,
                            Price=m.Price, //
                        }).ToList();

            return movies;
        }

        // Method to fetch movie details by id
        public MovieDetailModel GetMovieById(int movieId)
        {
            // Fetch movie details from the database
            var movie = _context.MovieDetails.FirstOrDefault(m => m.Movie_Id == movieId);

            //map it to MovieDetailModel
            if (movie != null)
            {
                Console.WriteLine($"Movie found: {movie.Movie_Name}");
                return new MovieDetailModel
                {
                    Movie_Id = movie.Movie_Id,
                    Movie_Name = movie.Movie_Name,
                    Language = movie.Language,
                    Genre = movie.Genre,
                    Director = movie.Director,
                    Cast = movie.Cast,
                    Descriptiom = movie.Descriptiom,
                    Image = movie.Image,
                    Price=movie.Price,
                };
            }
            Console.WriteLine("Movie not found");

            
            return null;

        }

        public List<MovieDetailModel> GetAllMoviesForSubscribedUser(bool isSubscribed)
        {
            var movies = _context.MovieDetails
                        .Select(m => new MovieDetailModel
                        {
                            Movie_Id = m.Movie_Id,
                            Movie_Name = m.Movie_Name,
                            Language = m.Language,
                            Genre = m.Genre,
                            Director = m.Director,
                            Cast = m.Cast,
                            Descriptiom = m.Descriptiom,
                            Image = m.Image,
                            Price = m.Price,
                        }).ToList();

            if (isSubscribed)
            {
                // If the user is subscribed, all movies are available
                return movies;
            }
            else
            {
                // If the user is not subscribed, only paid movies are available
                return movies.Where(m => decimal.TryParse(m.Price, out var price) && price > 0).ToList();
            }
        }

    }
}
