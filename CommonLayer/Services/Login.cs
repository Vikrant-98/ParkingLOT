using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Services
{
    public class Login
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]{2}[a-zA-Z0-9]*[.]{0,1}[a-zA-Z0-9]*@[a-zA-Z0-9]*.{1}[a-zA-Z0-9]*[.]*[a-zA-Z0-9]*)$", ErrorMessage = "Enter Valid Email")]
        public string MailID { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        public string Password { get; set; }
    }
}
