using BuissinessLogicLayer;
using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Movie2.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ManagePurchase _managePurchase;
        private readonly ManageMovie _manageMovie;

        public PurchaseController(ManagePurchase managePurchase, ManageMovie manageMovie)
        {
            _managePurchase = managePurchase;
            _manageMovie = manageMovie;
        }

        // GET: Show the purchase form
        [HttpGet]
        public IActionResult PurchaseForm(int movieId)
        {
            var userId = HttpContext.Session.GetInt32("User_Id");
            if (userId == null)
            {
                // Redirect to login if the user is not logged in
                return RedirectToAction("LogIn", "UserAccount");
            }

            // Fetch movie details by movieId
            var movie = _manageMovie.GetMovieById(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            // Prepare the purchase model with all the movie details and userId
            var purchaseModel = new UserPurchaseModel
            {
                Movie_Id = movie.Movie_Id,
                Movie_Name = movie.Movie_Name,
                Language = movie.Language,
                Genre = movie.Genre,
                Director = movie.Director,
                Cast = movie.Cast,
                Descriptiom = movie.Descriptiom,
                Image = movie.Image,
                Price = movie.Price,
                User_Id = userId.Value // Store the user ID
            };

            return View(purchaseModel);
        }


        // POST: Handle purchase form submission
        [HttpPost]
        public IActionResult SubmitPurchase(UserPurchaseModel purchaseModel)
        {
            var userId = HttpContext.Session.GetInt32("User_Id");
            if (userId == null)
            {
                // Redirect to login if the user is not logged in
                return RedirectToAction("LogIn", "UserAccount");
            }

            if (ModelState.IsValid)
            {
                // Complete the purchase by calling the ManagePurchase class
                _managePurchase.CompletePurchase(userId.Value, purchaseModel.Movie_Id);

                // Show a success message and redirect
                TempData["SuccessMessage"] = "Purchase was successful!";
                return RedirectToAction("MovieList", "Movie");
            }

            // If validation fails, show the form again
            return View("PurchaseForm", purchaseModel);
        }

        public IActionResult PastOrders()
        {
            var userId = HttpContext.Session.GetInt32("User_Id");
            if (userId == null)
            {
                return RedirectToAction("LogIn", "UserAccount");
            }

            // Fetch the user's past orders using ManagePurchase class
            var pastOrders = _managePurchase.GetUserPurchases(userId.Value);

            return View(pastOrders);
        }
    }
}
