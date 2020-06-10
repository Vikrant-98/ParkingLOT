using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class ParkingInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingID { get; set; }

        public int ParkingSlotNo { get; set; } 

        [Required]
        [RegularExpression(@"^[a-z||A-Z]{2}[ ]{1}[0-9]{2}[ ]{1}[a-z||A-Z]{1,2}[ ]{1}[0-9]{4}$", ErrorMessage = "Enter Valid Vehicle Number")]
        public string VehicalNo { get; set; } 
        [Required]
        public string VehicalBrand { get; set; } 

        public string VehicalColor { get; set; } 

        public double ChargePerHr { get; set; } 

        public DateTime EntryTime { get; set; } 

        public DateTime ExitTime { get; set; }

        public string ParkingType { get; set; } 

        public bool ParkStatus { get; set; }
    }
}
