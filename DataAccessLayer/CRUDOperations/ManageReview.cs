using System;
using System.Collections.Generic;
using System.Linq;
using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;

namespace DataAccessLayer.CRUDOperations
{
    public class ManageReview
    {
        private readonly ApplicationDbContext _context;

        public ManageReview(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to prepare the review model based on movie and user details
        public ReviewModel PrepareReviewModel(int movieId, int userId)
        {
            var movie = _context.MovieDetails.FirstOrDefault(m => m.Movie_Id == movieId);
            if (movie == null) throw new Exception("Movie not found.");

            // Prepare and return the review model
            return new ReviewModel
            {
                Movie_Id = movieId,
                User_Id = userId,
                Movie_Name = movie.Movie_Name,
                Language = movie.Language,
                Genre = movie.Genre,
                Director = movie.Director,
                Cast = movie.Cast,
                Descriptiom = movie.Descriptiom,
                Image = movie.Image,
                Price = movie.Price
            };
        }

        // Method to add a review
        public void AddReview(ReviewModel reviewModel)
        {
            var review = new Review
            {
                Review_Description = reviewModel.Review_Description,
                Movie_Id = reviewModel.Movie_Id,
                User_Id = reviewModel.User_Id,
                Movie_Name = reviewModel.Movie_Name,
                Language = reviewModel.Language,
                Genre = reviewModel.Genre,
                Director = reviewModel.Director,
                Cast = reviewModel.Cast,
                Descriptiom = reviewModel.Descriptiom,
                Image = reviewModel.Image,
                Price = reviewModel.Price
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        // Method to get all reviews for a specific movie
        public List<ReviewModel> GetReviewsByMovieId(int movieId)
        {
            return _context.Reviews
                .Where(r => r.Movie_Id == movieId)
                .Select(r => new ReviewModel
                {
                    Review_Id = r.Review_Id,
                    Review_Description = r.Review_Description,
                    Movie_Id = r.Movie_Id,
                    User_Id = r.User_Id,
                    Movie_Name = r.Movie_Name,
                    Language = r.Language,
                    Genre = r.Genre,
                    Director = r.Director,
                    Cast = r.Cast,
                    Descriptiom = r.Descriptiom,
                    Image = r.Image,
                    Price = r.Price
                })
                .ToList();
        }
    }
}
