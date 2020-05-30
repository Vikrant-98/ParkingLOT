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
        [Required(ErrorMessage = "DriverCategory Is Required")]
        [MaxLength(50)]
        public Driver DriverCategory { get; set; }
        [Required]
        [RegularExpression(@"^[a-z]{2}[0-9]{2}[a-z]{1,2}[0-9]{4}$", ErrorMessage = "Enter Valid Email")]
        public int VehicalNo { get; set; }
        [Required]
        public string VehicalBrand { get; set; }
        
        public string VehicalColor { get; set; }

        public int ChargePerHr { get; set; }

        public DateTime EntryTime { get; set; } = DateTime.Now;

        public DateTime ExitTime { get; set; }
        [Required]
        public Parkingtype ParkingType { get; set; }
    }
}
