using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCommonLayer.ResponseModels
{
    public class RegistrationResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MailID { get; set; }

        public string DriverCategory { get; set; }

        public DateTime CreateDate { get; set; } 
    }
}
