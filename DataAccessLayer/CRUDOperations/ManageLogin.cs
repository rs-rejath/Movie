using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.CRUDOperations
{
    public class ManageLogin
    {
        private readonly ApplicationDbContext _context;
      
        public ManageLogin(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public UserAccount CheckLogin(string email, string password)
        {
            // Check if a user with the  email and password exists in db
            return _context.UserAccounts
                .FirstOrDefault(a => a.Email == email && a.Password == password);
        }


    }
}
