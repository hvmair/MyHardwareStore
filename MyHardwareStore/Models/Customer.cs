using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHardwareStore
{
    public class Customer
    {
        public int custId { get; set; }

        [Required (ErrorMessage = "Please Enter a First Name")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }


        [Display(Name = "Middle Name")]
        public string middleName { get; set; }

        [Required(ErrorMessage = "Please Enter a Last Name")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please Enter an Address")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Suite/Apt./Flr")]
        public string address2 { get; set; }

        [Required(ErrorMessage = "Please Enter a City")]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required(ErrorMessage = "Please Enter a State")]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required(ErrorMessage = "Please Enter a Zip Code")]
        [Display(Name = "Zip Code")]
        public int zipCode { get; set; }

    }
}