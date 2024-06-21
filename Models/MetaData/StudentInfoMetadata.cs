using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project2_implement_Theme.Models.MetaData
{


    public class StudentInfoMetadata
    {
        [Required(ErrorMessage = "Please enter a your Name.")]
        [StringLength(50,ErrorMessage = "Please do not enter values over 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a City Name.")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a PIN code.")]
        [StringLength(6,MinimumLength =6,ErrorMessage = "Invalid PIN code. Must be 6 digits.")]
        public int PinCode { get; set; }
    }
}