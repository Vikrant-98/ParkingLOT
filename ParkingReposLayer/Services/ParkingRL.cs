using ParkingCommonLayer.Services;
using ParkingReposLayer.ApplicationDB;
using ParkingReposLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingReposLayer.Services
{
    public class ParkingRL : IParkingRL
    {
        /// <summary>
        /// Dependency Injection from application and repos layer
        /// </summary>
        private Application dBContext;
        readonly Random random = new Random();
        public ParkingRL(Application dBContext)
        {
            this.dBContext = dBContext;
        }
        /// <summary>
        /// Park the Vehicle 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool ParkVehicle(Information Info)
        {
            try
            {
                bool parkingtype = Enum.TryParse<Parkingtype>(Info.ParkingType, true, out Parkingtype Parkingtype);
                if (parkingtype != true)
                {
                    throw new Exception("Invalid Parking Type");
                }
                string VehicalNo = Info.VehicalNumber;
                var Validation = dBContext.ParkingInfo.Where(u => u.VehicalNo == Info.VehicalNumber).FirstOrDefault();
                
                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     //throw exception when user exist
                }
                int ParkingSlotNo = AllotcateSlot();
                if (ParkingSlotNo == 0)
                {
                    throw new Exception("Parking is Full ");
                }
                ParkingInformation data = new ParkingInformation
                {
                    EntryTime = DateTime.Now,
                    VehicalNo = Info.VehicalNumber,
                    VehicalBrand = Info.VehicalBrand,
                    VehicalColor = Info.VehicalColor,
                    ParkingType = Info.ParkingType,
                    ParkingSlotNo = ParkingSlotNo,
                    ParkStatus = true
                };
                
                var Result = dBContext.ParkingInfo.Add(data);
               
                if (Result != null)
                {
                    dBContext.SaveChanges();
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
        /// Alloting the slot number
        /// </summary>
        /// <returns></returns>
        public int AllotcateSlot()
        {
            int slot = 1;
            int count = 0;
            bool flag = false;
            while (flag != true || count != 100)
            {
                slot = (random.Next() % 100) + 1;
                var result = dBContext.ParkingInfo.Where(u => u.ParkingSlotNo == slot).FirstOrDefault();
                if (result == null)
                {
                    flag = true;                     
                }
                count++;
            }
            if (count < 101)
                return slot;
            else
                return 0;
        }
        /// <summary>
        /// Unpark the vehicle 
        /// Exittime modified
        /// charge get calculated
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool UnparkVehicle(Unpark Info)
        {
            try
            {
                var Entries = (from x in dBContext.ParkingInfo
                               where x.VehicalNo == Info.VehicalNo
                               select x).First();

                if (Entries.ParkStatus == false)
                {
                    throw new Exception("Unparked Already");
                }

                if (Entries != null)
                {
                    Entries.ParkingSlotNo = 0;
                    Entries.ExitTime = DateTime.Now;
                    double timeDiff = Entries.ExitTime.Subtract(Entries.EntryTime).TotalHours;
                    Entries.ChargePerHr = timeDiff * 8;
                    Entries.ParkStatus = false;
                    dBContext.SaveChanges();
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
        /// Giving the All Parking Details in form of list
        /// </summary>
        /// <returns></returns>
        public object GetAllParkingData()
        {
            try
            {
                var allEntries = dBContext.ParkingInfo;

                return allEntries;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }
        /// <summary>
        /// Giving the All Parked Details in form of list
        /// </summary>
        /// <returns></returns>
        public object GetAllParkedData()
        {
            try
            {
                var allEntries = dBContext.ParkingInfo.Where(x => x.ParkStatus == true);

                return allEntries;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }
        /// <summary>
        /// Giving the All UnParked Details in form of list
        /// </summary>
        /// <returns></returns>
        public object GetAllUnParkedData()
        {
            try
            {
                var allEntries = dBContext.ParkingInfo.Where(x => x.ParkStatus == false);

                return allEntries;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }
        /// <summary>
        /// Deleted Parking Details
        /// </summary>
        /// <param name="ReceiptNumber"></param>
        /// <returns></returns>
        public object DeleteCarParkingDetails(int ReceiptNumber)
        {
            try
            {
                var details = dBContext.ParkingInfo.First(x => x.ParkingID == ReceiptNumber);

                if (details != null)
                {
                    dBContext.ParkingInfo.Remove(details);

                    dBContext.SaveChanges();
                    return details;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Update Parking Details
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public object UpdateParkingRecord(Information Info, int ID)
        {
            try
            {
                string VehicalNo = Info.VehicalNumber;
                var Validation = dBContext.ParkingInfo.Where(u => u.VehicalNo == VehicalNo && u.ParkingID != ID).FirstOrDefault();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     
                }

                var Entries = (from x in dBContext.ParkingInfo
                               where x.ParkingID == ID
                               select x).First();
                if (Entries != null)
                {
                    Entries.ParkingType = Info.ParkingType;
                    Entries.VehicalBrand = Info.VehicalBrand;
                    Entries.VehicalNo = Info.VehicalNumber;
                    Entries.VehicalColor = Info.VehicalColor;
                    dBContext.SaveChanges();
                    return Entries;
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
        /// Get Car Details by Vehicle Number
        /// </summary>
        /// <param name="VehicleNo"></param>
        /// <returns></returns>
        public object GetCarDetailsByVehicleNumber(string VehicleNo)
        {
            ParkingInformation detail = new ParkingInformation();
            detail.VehicalNo = VehicleNo;
            try
            {
                if (dBContext.ParkingInfo.Any(x => x.VehicalNo == VehicleNo))
                {
                    return (from Details in dBContext.ParkingInfo
                            where Details.VehicalNo == VehicleNo
                            select Details).ToList();
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Get car details by Vehicle Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public object GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                if (dBContext.ParkingInfo.Any(x => x.VehicalBrand == brand))
                {

                    var VehicleData = (from Details in dBContext.ParkingInfo
                                       where Details.VehicalBrand == brand
                                       select Details).ToList();

                    return VehicleData;
                }
                else
                {
                    
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        
    }
}
