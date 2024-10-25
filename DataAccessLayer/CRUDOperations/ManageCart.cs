using DataAccessLayer.ApplicationContext;
using System.Linq;
using BuissinessLogicLayer;

namespace DataAccessLayer.CRUDOperations
{
    public class ManageCart
    {
        private readonly ApplicationDbContext _context;

        public ManageCart(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public void AddMovieToCart(int userId, int movieId)
        {
            // Get the movie details from db
            var movie = _context.MovieDetails.SingleOrDefault(m => m.Movie_Id == movieId);

            if (movie == null)
            {
                
                throw new Exception("Movie not found.");
            }

            // Check if the movie is already in the cart
            var existingCartItem = _context.Carts.SingleOrDefault(c => c.Movie_Id == movieId && c.User_Id == userId);

            if (existingCartItem == null)
            {
                // If the movie is not in the cart add it tp cart
                var cartItem = new Cart
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

                _context.Carts.Add(cartItem);
                _context.SaveChanges();
            }
            

            

        }

        // Method to get the user's cart items
        public List<CartModel> GetUserCart(int userId)
        {
            
            return _context.Carts
                .Where(c => c.User_Id == userId)
                .Select(c => new CartModel
                {
                    Movie_Id = c.Movie_Id,
                    Movie_Name = c.Movie_Name,
                    Language = c.Language,
                    Genre = c.Genre,
                    Director = c.Director,
                    Cast = c.Cast,
                    Descriptiom = c.Descriptiom,
                    Image = c.Image,
                    Price=c.Price,
                })
                .ToList();
        }

        public void RemoveMovieFromCart(int userId, int movieId)
        {
            // Find the cart item that matches the user and movie to remove it
            var cartItem = _context.Carts.SingleOrDefault(c => c.User_Id == userId && c.Movie_Id == movieId);

            if (cartItem != null)
            {
                
                _context.Carts.Remove(cartItem);

                
                _context.SaveChanges();
            }
            else
            {
               
                throw new Exception("Movie not found in cart.");
            }
        }

    }
}
