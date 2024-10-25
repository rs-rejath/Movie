using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using BuissinessLogicLayer;

namespace Movie2.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ManageUserProfile _manageUserProfile;

        public SubscriptionController(ManageUserProfile manageUserProfile)
        {
            _manageUserProfile = manageUserProfile;
        }

        // Redirect to the payment form for subscription
        [HttpGet]
        public IActionResult Subscribe()
        {
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                return RedirectToAction("Login", "UserAccount");
            }

            // Redirect to the payment form
            return RedirectToAction("PaymentFormSubscription");
        }

        // Show the payment form
        [HttpGet]
        public IActionResult PaymentFormSubscription()
        {
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                return RedirectToAction("Login", "UserAccount");
            }

            return View(); 
        }

        // Process the payment and update subscription status
        [HttpPost]
        public IActionResult ProcessSubscription(SubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                

                // Update the user's subscription status after payment
                var userId = HttpContext.Session.GetInt32("User_Id");

                if (userId != null)
                {
                    var user = _manageUserProfile.GetUserById(userId.Value);

                    if (user != null)
                    {
                        user.SubscriptionStatus = true; // Mark as subscribed
                        _manageUserProfile.UpdateUser(user);
                    }
                }

                // Redirect after successful subscription
                return RedirectToAction("MovieList", "Movie");
            }

            // If payment fails, return to the payment form with validation errors
            return View("PaymentFormSubscription", model);
        }
    }
}
