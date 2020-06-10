using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCommonLayer.ResponseModels
{
    public class LoginResponse
    {
        public string mail { get; set; }

        public string token { get; set; }
    }
}
