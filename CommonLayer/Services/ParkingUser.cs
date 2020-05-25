using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Services
{
    [Table("User")]
    public class ParkingUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]*[.]*[a-zA-Z0-9]*@[a-zA-Z0-9]*.{1}[a-zA-Z0-9]*[.]*[a-zA-Z0-9]*)$", ErrorMessage = "Enter Valid Email")]
        public string MailID { get; set; }
        [Required(ErrorMessage = "DriverCategory Is Required")]
        [MaxLength(50)]
        public string DriverCategory { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        public string Password { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        
        public DateTime ModifiedDate { get; set; }
    }
}
