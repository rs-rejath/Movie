using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;
using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace Movie2.Controllers
{
    public class Movie : Controller
    {
        private readonly ManageMovie _manageMovie;
        private readonly ManageUserProfile _manageUserProfile;

        public Movie(ManageMovie manageMovie, ManageUserProfile manageUserProfile)
        {
            _manageMovie = manageMovie;
            _manageUserProfile = manageUserProfile;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action to display the entire list of movies
        public IActionResult MovieList(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 3;

            // Check if the user is subscribed
            var userId = HttpContext.Session.GetInt32("User_Id");
            bool isSubscribed = false;

            if (userId != null)
            {
                var user = _manageUserProfile.GetUserById(userId.Value);
                isSubscribed = user?.SubscriptionStatus ?? false;
            }

            // Fetch all movies, showing all movies if subscribed
            List<MovieDetailModel> movies = _manageMovie.GetAllMoviesForSubscribedUser(isSubscribed);
            var pagedMovies = movies.ToPagedList(pageNumber, pageSize);

            return View(pagedMovies);
        }
        // Action for subscribing the user
        public IActionResult Subscribe()
        {
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                return RedirectToAction("LogIn", "UserAccount");
            }

            // Subscribe the user (updating the IsSubscribed field)
            _manageUserProfile.SubscribeUser(userId.Value);

            return RedirectToAction("MovieList"); // Redirect to the movie list page
        }

    }
}
