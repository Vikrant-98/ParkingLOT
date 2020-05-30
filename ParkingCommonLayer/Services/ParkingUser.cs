using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingCommonLayer.Services
{
    [Table("User")]
    public class ParkingUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Unique ID get generated using auto Increment
        public int ID { get; set; }
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
        public Driver DriverCategory { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        //Password
        public string Password { get; set; }
        //Create date 
        public DateTime CreateDate { get; set; } = DateTime.Now;
        //Modified Date
        public DateTime ModifiedDate { get; set; }
    }
    public class EncryptedPassword
    {
        /// <summary>
        /// Encrypt the Password logic
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;                                         //Return Encrypted Data
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
    public enum Driver
    {
        Owner,
        Security,
        Police,
        Driver
    }
    public enum Parkingtype
    {
        Owner,
        Vallet
    }
}
