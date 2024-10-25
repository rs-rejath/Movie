using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissinessLogicLayer
{
    public class MovieDetailModel
    {
        [Key]
        public int Movie_Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string Movie_Name { get; set; }

        [Required(ErrorMessage = "Language is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Language { get; set; }


        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Director is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Director is required")]
        [MaxLength(500, ErrorMessage = "Max 500 characters allowed")]
        public string Cast { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(2000, ErrorMessage = "Max 2000 characters allowed")]
        public string Descriptiom { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [MaxLength(2000, ErrorMessage = "Max 2000 characters allowed")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string Price { get; set; }


    }
}
