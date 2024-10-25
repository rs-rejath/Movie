using DataAccessLayer.ApplicationContext;

using System;
using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;

namespace DataAccessLayer.CRUDOperations
{
    public class ManagePurchase
    {
        private readonly ApplicationDbContext _context;

        public ManagePurchase(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to handle the purchase and update the UserPurchases table
        public void CompletePurchase(int userId, int movieId)
        {
            // Get the movie details from the MovieDetails table
            var movie = _context.MovieDetails.SingleOrDefault(m => m.Movie_Id == movieId);

            if (movie == null)
            {
                throw new Exception("Movie not found.");
            }

            // Create a new UserPurchase record
            var purchase = new UserPurchase
            {
                User_Id = userId,
                Movie_Id = movieId,
                Movie_Name = movie.Movie_Name,
                Language = movie.Language,
                Genre = movie.Genre,
                Director = movie.Director,
                Cast = movie.Cast,
                Descriptiom = movie.Descriptiom,
                Image = movie.Image,
                Price=movie.Price,
            };

            
            _context.UserPurchases.Add(purchase);
            _context.SaveChanges();
        }

        // Method to get all purchases by an user
        public List<UserPurchaseModel> GetUserPurchases(int userId)
        {
            // Fetch all purchases made by the user from db
            return _context.UserPurchases
                .Where(p => p.User_Id == userId)
                .Select(p => new UserPurchaseModel
                {
                    Purchase_Id = p.Purchase_Id,
                    Movie_Name = p.Movie_Name,
                    Language = p.Language,
                    Genre = p.Genre,
                    Director = p.Director,
                    Cast = p.Cast,
                    Descriptiom = p.Descriptiom,
                    Image = p.Image,
                    Price = p.Price
                })
                .ToList();
        }
    }
}
