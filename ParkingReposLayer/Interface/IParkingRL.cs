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
        bool AddUser(ParkingUser Info);
        //Login
        bool LoginVerification(Login Info);

        bool ParkVehicle(ParkingInformation Info);

        bool UnparkVehicle(Unpark Info);

        List<ParkingInformation> GetAllParkingData();

        object DeleteCarParkingDetails(int ReceiptNumber);

        bool UpdateRecord(ParkingInformation Info,int ID);

        object GetCarDetailsByVehicleNumber(string VehicleNumber);

        object GetCarDetailsByVehicleBrand(string brand);
    }
}
