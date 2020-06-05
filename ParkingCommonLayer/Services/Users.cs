using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class Users
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid First Name")]
        //First Name
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid Last Name")]
        //Last Name
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]{2}[a-zA-Z0-9]*[.]{0,1}[a-zA-Z0-9]*@[a-zA-Z0-9]*.{1}[a-zA-Z0-9]*[.]*[a-zA-Z0-9]*)$", ErrorMessage = "Enter Valid Email")]
        //Mail ID
        public string MailID { get; set; }
        [Required(ErrorMessage = "DriverCategory Is Required")]
        [MaxLength(50)]
        //Driver Categiry
        public string DriverCategory { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        //Password
        public string Password { get; set; }
       
    }
}
