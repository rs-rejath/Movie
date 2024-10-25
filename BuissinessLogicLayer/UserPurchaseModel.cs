using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissinessLogicLayer
{
    public class UserPurchaseModel
    {
        public int Movie_Id { get; set; }
        public int User_Id { get; set; }

        public string CardNumber { get; set; }

        public string Expiry { get; set; }

        public string CVV { get; set; }

        public string CardHolderName { get; set; }

        public int Purchase_Id { get; set; }
        public string Movie_Name { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Descriptiom { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
    }
}
