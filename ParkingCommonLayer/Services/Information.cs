using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class Information
    {
        //Vehicle number
        [Required]
        [RegularExpression(@"^[a-z||A-Z]{2}[ ]{1}[0-9]{2}[ ]{1}[a-z||A-Z]{1,2}[ ]{1}[0-9]{4}$", ErrorMessage = "Enter Valid Vehicle Number")]
        public string VehicalNumber { get; set; }
        //Vehicle Brand
        [Required]
        public string VehicalBrand { get; set; }
        //Vehicle Color
        [Required]
        public string VehicalColor { get; set; }
        //Parking type
        public string ParkingType { get; set; }
    }
}
