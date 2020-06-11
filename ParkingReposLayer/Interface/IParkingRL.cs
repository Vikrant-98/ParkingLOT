using ParkingCommonLayer.Services;
using System.Collections.Generic;


namespace ParkingReposLayer.Interface
{
    /// <summary>
    /// Declare Interface for Registration and login user
    /// </summary>
    public interface IParkingRL
    {
        bool ParkVehicle(Information Info);

        bool UnparkVehicle(Unpark Info);

        object GetAllParkingData();

        object GetAllParkedData();

        object GetAllUnParkedData();

        object DeleteCarParkingDetails(int ReceiptNumber);

        object UpdateParkingRecord(Information Info,int ID);

        object GetCarDetailsByVehicleNumber(string VehicleNumber);

        object GetCarDetailsByVehicleBrand(string brand);
    }
}
