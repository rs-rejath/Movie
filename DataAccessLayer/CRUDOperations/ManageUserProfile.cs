using DataAccessLayer.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CRUDOperations
{
    public class ManageUserProfile
    {
        private readonly ApplicationDbContext _context;

        public ManageUserProfile(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserAccount GetUserById(int userId)
        {
            // Fetch user details by ID
            return _context.UserAccounts.FirstOrDefault(u => u.User_Id == userId);
        }

        // Update user profile details
        public void UpdateUser(UserAccount user)
        {
            // Fetch the existing user from the database
            var existingUser = _context.UserAccounts.FirstOrDefault(u => u.User_Id == user.User_Id);

            if (existingUser != null)
            {
                // Update the properties with the new values from the passed 'user' object
                existingUser.User_Name = user.User_Name;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Street = user.Street;
                existingUser.City = user.City;
                existingUser.PostCode = user.PostCode;
                existingUser.Password = user.Password;

               
                _context.UserAccounts.Update(existingUser);

                // Save changes to the database
                _context.SaveChanges();
            }
        }

        // Method to subscribe a user
        public void SubscribeUser(int userId)
        {
            var user = _context.UserAccounts.FirstOrDefault(u => u.User_Id == userId);
            if (user != null)
            {
                user.SubscriptionStatus = true;
                _context.SaveChanges();
            }
        }
    }
}
