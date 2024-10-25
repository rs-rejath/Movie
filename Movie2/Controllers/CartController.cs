using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;
using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Movie2.Models;

namespace Movie2.Controllers
{
    public class CartController : Controller
    {
        private readonly ManageCart _manageCart;
        private readonly ManageMovie _manageMovie;

        public CartController(ManageCart manageCart, ManageMovie manageMovie)
        {
            _manageCart = manageCart;
            _manageMovie = manageMovie;
        }

        // Action to add a movie to the cart
        [HttpPost]
        public IActionResult AddToCart(int movieId)
        {
            // Get the user ID from the session
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                // If the user is not logged in, redirect to login page
                return RedirectToAction("LogIn", "UserAccount");
            }

            // Check if the movie exists in the database before adding to cart
            var movie = _manageMovie.GetMovieById(movieId);
            if (movie == null)
            {
                // If movie not found, show an error page or handle appropriately
                return View("Error", new ErrorViewModel { RequestId = "Movie not found." });
            }

            try
            {
                // Call the method to add the movie to the cart
                _manageCart.AddMovieToCart(userId.Value, movieId);

                // Redirect back to the movie list or the cart view
                return RedirectToAction("MovieList", "Movie");
            }
            catch (Exception ex)
            {
                // Log the exception and show an error page
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = "Error adding movie to the cart." });
            }
        }


        // Action to view the cart
        public IActionResult ViewCart()
        {
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                return RedirectToAction("LogIn", "UserAccount");
            }

            // Get the user's cart items
            var cartItems = _manageCart.GetUserCart(userId.Value);

            return View(cartItems);
        }

        public IActionResult RemoveFromCart(int movieId)
        {
            // Get the user ID from the session
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                // If the user is not logged in, redirect to the login page
                return RedirectToAction("LogIn", "UserAccount");
            }

            try
            {
                // Call the method to remove the movie from the cart
                _manageCart.RemoveMovieFromCart(userId.Value, movieId);

                // Redirect back to the cart view
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {
                // Log the exception and show an error page
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = "Error removing movie from cart." });
            }
        }

    }
}
