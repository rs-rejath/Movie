using BuissinessLogicLayer;
using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;

namespace Movie2.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ManageReview _manageReview;

        public ReviewController(ManageReview manageReview)
        {
            _manageReview = manageReview;
        }

       //  Review form
        [HttpGet]
        public IActionResult AddReview(int movieId)
        {
            var userId = HttpContext.Session.GetInt32("User_Id");
            if (userId == null)
            {
                return RedirectToAction("LogIn", "UserAccount");
            }

            
            var reviewModel = _manageReview.PrepareReviewModel(movieId, userId.Value);
            return View(reviewModel);
        }

        //  Submit the review
        [HttpPost]
        public IActionResult AddReview(ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                _manageReview.AddReview(reviewModel);
                return RedirectToAction("MovieList", "Movie");
            }

            
            return View(reviewModel);
        }

        // Display all reviews for a  movie
        [HttpGet]
        public IActionResult ViewReviews(int movieId)
        {
            var reviews = _manageReview.GetReviewsByMovieId(movieId);
            if (!reviews.Any())
            {
                ViewBag.Message = "No reviews available for this movie.";
            }

            return View(reviews);
        }
    }
}

