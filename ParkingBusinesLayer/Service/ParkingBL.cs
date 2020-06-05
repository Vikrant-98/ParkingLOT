using ParkingBusinesLayer.Interface;
using ParkingCommonLayer.Services;
using ParkingReposLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingBusinesLayer.Service
{
    public class ParkingBL : IParkingBL
    {
        /// <summary>
        /// Dapandency Injection perform on Constructor form Repos layer and  business layer
        /// </summary>
        private IParkingRL Parking;
        public ParkingBL(IParkingRL data)
        {
            Parking = data;
        }

        public bool ParkVehicle(ParkingInformation Info)
        {
            try
            {
                var Result = Parking.ParkVehicle(Info);                               //get result true or false
                if (Result == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UnparkVehicle(Unpark Info)
        {
            try
            {
                var Result = Parking.UnparkVehicle(Info);                               //get result true or false
                if (Result == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ParkingInformation> GetAllParkingData()
        {
            return Parking.GetAllParkingData();
        }

        public object DeleteCarParkingDetails(int ReceiptNumber)
        {
            try
            {
                var data = Parking.DeleteCarParkingDetails(ReceiptNumber);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception();
                }


            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);

            }
        }
        
        public object UpdateParkingRecord(Information Info,int ID)
        {
            try
            {
                var Result = Parking.UpdateParkingRecord(Info,ID);                               //get result true or false
                if (Result != null)
                {
                    return Result;
                }
                else
                {
                    throw new Exception("Record not updated!!!!!!!!!!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object GetCarDetailsByVehicleNumber(string VehicleNumber)
        {
            try
            {
                var data = Parking.GetCarDetailsByVehicleNumber(VehicleNumber);
                // Check IF Data Equal To Null 
                if (data != null)
                {
                    // IF Data Null Throw Exception
                    return data;
                }
                else
                {
                    // Return
                    throw new Exception();

                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }
        public object GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                var data = Parking.GetCarDetailsByVehicleBrand(brand);
                // Check IF Data Equal To Null Or Not
                if (data != null)
                {
                    // Return data
                    return data;
                }
                else
                {
                    // IF Data Null Throw Exception
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }
    }
}
