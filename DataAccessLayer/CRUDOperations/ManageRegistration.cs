using BuissinessLogicLayer;
using DataAccessLayer.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CRUDOperations
{
    public class ManageRegistration
    {
        private readonly ApplicationDbContext _context;
      
        public ManageRegistration(ApplicationDbContext context)
        {
            _context = context;
        }
        public void addRegistration(RegistrationModel model)
        {
            UserAccount userAccount = new UserAccount();
            userAccount.User_Name = model.User_Name;
            userAccount.Email = model.Email;
            userAccount.Phone= model.Phone;
            userAccount.Street = model.Street;
            userAccount.City = model.City;
            userAccount.PostCode = model.PostCode;
            userAccount.Password = model.Password;
            _context.UserAccounts.Add(userAccount);
            _context.SaveChanges();
        }
    }
}
