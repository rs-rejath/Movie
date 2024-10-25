using BuissinessLogicLayer;
using DataAccessLayer.CRUDOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Movie2.Controllers
{
    public class UserAccount : Controller
    {
        private readonly ManageRegistration _manageRegistration;
        private readonly ManageLogin _manageLogin;
        private readonly ManageUserProfile _manageUserProfile;
        //private readonly IHttpContextAccessor _contx;
        public UserAccount(ManageRegistration manageRegistration, ManageLogin manageLogin/*, IHttpContextAccessor contx*/, ManageUserProfile manageUserProfile)
        {

            _manageRegistration = manageRegistration;
            _manageLogin = manageLogin;
            //_contx = contx;
            _manageUserProfile = manageUserProfile;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                _manageRegistration.addRegistration(model); 
                ModelState.Clear();
                TempData["SuccessMessage"] = "Registered sucessfully";
                return RedirectToAction("LogIn","UserAccount");

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            var userAccount = _manageLogin.CheckLogin(email, password);

            if (userAccount != null)
            {
                // Store User_Id and User_Name in the session
               /* _contx.*/HttpContext.Session.SetInt32("User_Id", userAccount.User_Id); // Assuming User_Id is an integer
                /*_contx.*/HttpContext.Session.SetString("User_Name", userAccount.User_Name);
                HttpContext.Session.SetString("Subscription_Status", userAccount.SubscriptionStatus.ToString());


               
                return RedirectToAction("Index", "Home");
            }
            else
            {
               
                ViewBag.ErrorMessage = "Invalid Email or Password";
                return View();
            }
        }

		public IActionResult Logout()
		{
			
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "UserAccount");
		}

        public IActionResult ProfileDetails()
        {
            // Take the user information from session 
            var userId = HttpContext.Session.GetInt32("User_Id");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            // Fetch the user details from ManageUserProfile 
            var user = _manageUserProfile.GetUserById(userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            // Pass user details to the view
            return View(user);
        }

        //  Edit Profile form
        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            // Fetch user details by  ID
            var user = _manageUserProfile.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Update user details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(DataAccessLayer.ApplicationContext.UserAccount model)
        {
            if (ModelState.IsValid)
            {
                _manageUserProfile.UpdateUser(model); // Update detailw in the db
                return RedirectToAction("ProfileDetails", new { id = model.User_Id });
            }
            

            return View(model); // If the model is invalid, return to the edit page with validation errors
        }


    }
}

