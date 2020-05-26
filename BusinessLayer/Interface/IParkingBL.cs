using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IParkingBL
    {
        bool ParkingRegisterDatails(ParkingUser Info);

        bool ParkingLoginDatails(Login Info);
    }
}
