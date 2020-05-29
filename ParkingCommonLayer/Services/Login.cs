
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ParkingCommonLayer.Services
{
    public class Login
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]{2}[a-zA-Z0-9]*[.]{0,1}[a-zA-Z0-9]*@[a-zA-Z0-9]*.{1}[a-zA-Z0-9]*[.]*[a-zA-Z0-9]*)$", ErrorMessage = "Enter Valid Email")]
        //Mail ID
        public string MailID { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        //Password
        public string Password { get; set; }
    }
}
