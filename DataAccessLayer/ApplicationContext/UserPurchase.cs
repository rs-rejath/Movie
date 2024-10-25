using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ApplicationContext
{
    public class UserPurchase
    {
        [Key]
        public int Purchase_Id { get; set; }

        

        public string Movie_Name { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Descriptiom { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
       
        public int Movie_Id { get; set; }
       
        public int User_Id { get; set; }
        // Navigation properties 
        [ForeignKey("User_Id")]
        public virtual UserAccount UserAccount { get; set; }
        [ForeignKey("Movie_Id")]
        public virtual MovieDetail MovieDetails { get; set; }
    }
}
