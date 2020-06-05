using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class Information
    {
        public int ParkingSlotNo { get; set; }

        [Required]
        [RegularExpression(@"^[a-z||A-Z]{2}[ ]{1}[0-9]{2}[ ]{1}[a-z||A-Z]{1,2}[ ]{1}[0-9]{4}$", ErrorMessage = "Enter Valid Vehicle Number")]
        public string VehicalNumber { get; set; }
        [Required]
        public string VehicalBrand { get; set; }

        public string VehicalColor { get; set; }

        public int ChargePerHr { get; set; }

        public string ParkingType { get; set; }
    }
}
