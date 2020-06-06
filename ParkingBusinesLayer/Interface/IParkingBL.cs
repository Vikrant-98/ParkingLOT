﻿using ParkingCommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingBusinesLayer.Interface
{
    /// <summary>
    /// Declare Interface for Registration and login user
    /// </summary>
    public interface IParkingBL
    {

        bool ParkVehicle(ParkingInformation Info);

        bool UnparkVehicle(Unpark Info);

        List<ParkingInformation> GetAllParkingData();

        List<ParkingInformation> GetAllParkData();

        List<ParkingInformation> GetAllUnParkData();

        object DeleteCarParkingDetails(int ReceiptNumber);
        
        object UpdateParkingRecord(Information Info, int ID);

        object GetCarDetailsByVehicleNumber(string VehicleNumber);

        object GetCarDetailsByVehicleBrand(string brand);
    }
}
