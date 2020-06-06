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
        /// <summary>
        /// Park the vehicle
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Unpark the vehicle
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Return list for Parking details
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllParkingData()
        {
            //Return Get all Details
            return Parking.GetAllParkingData();
        }
        /// <summary>
        /// Return list for Parking details
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllParkData()
        {
            //Return Get all Details
            return Parking.GetAllParkData();
        }
        /// <summary>
        /// Return list for Parking details
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllUnParkData()
        {
            //Return Get all Details
            return Parking.GetAllUnParkData();
        }
        /// <summary>
        /// Delete Parking Details
        /// </summary>
        /// <param name="ReceiptNumber"></param>
        /// <returns></returns>
        public object DeleteCarParkingDetails(int ReceiptNumber)
        {
            try
            {
                var data = Parking.DeleteCarParkingDetails(ReceiptNumber);
                // Check IF Data Equal To Null 
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
        /// <summary>
        /// Update Parking Record
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get car details by Vehicle Number
        /// </summary>
        /// <param name="VehicleNumber"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get car details using brands
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
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
