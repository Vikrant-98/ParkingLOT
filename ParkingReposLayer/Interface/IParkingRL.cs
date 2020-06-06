using ParkingCommonLayer.Services;
using System.Collections.Generic;


namespace ParkingReposLayer.Interface
{
    /// <summary>
    /// Declare Interface for Registration and login user
    /// </summary>
    public interface IParkingRL
    {
        bool ParkVehicle(ParkingInformation Info);

        bool UnparkVehicle(Unpark Info);

        List<ParkingInformation> GetAllParkingData();

        List<ParkingInformation> GetAllParkData();

        List<ParkingInformation> GetAllUnParkData();

        object DeleteCarParkingDetails(int ReceiptNumber);

        object UpdateParkingRecord(Information Info,int ID);

        object GetCarDetailsByVehicleNumber(string VehicleNumber);

        object GetCarDetailsByVehicleBrand(string brand);
    }
}
