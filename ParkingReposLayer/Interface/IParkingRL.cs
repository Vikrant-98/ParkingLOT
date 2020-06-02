using ParkingCommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingReposLayer.Interface
{
    /// <summary>
    /// Declare Interface for Registration and login user
    /// </summary>
    public interface IParkingRL
    {
        //Registration
        bool Addparking(ParkingUser Info);
        //Login
        bool LoginVerification(Login Info);

        bool ParkVehicle(ParkingInformation Info);

        List<ParkingInformation> GetAllParkingData();

        bool DeleteRecord(string VehicleNo);

        bool UpdateRecord(ParkingInformation Info);
    }
}
