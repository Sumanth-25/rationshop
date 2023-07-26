using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomesticShop.Models
{
    public class DomesticModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Message { get; set; }
    }

    public class Datamodel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public long PhoneNumber { get; set; }

        [Display(Name = "Aadhaar Number")]
        public long AadhaarNumber { get; set; }

        [Display(Name = "Ration Card Number")]
        public long RationCardNumber { get; set; }
        public string Email { get; set; }
        public string Things { get; set; }
    }

    public class MailRecord
    {
        public int Id { get; set; }
        public string message { get; set; }
        public string Email { get; set; }

        [Display(Name = "Date Time")]
        public string SentDate { get; set; }
    }
}