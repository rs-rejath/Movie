using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ApplicationContext
{
    public class UserAccount
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "Max 10 characters allowed")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "PostCode is required")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string Password { get; set; }

        public bool SubscriptionStatus { get; set; }
    }
}
