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
        public bool ParkVehicle(ParkingInformation Info)
        {
            try
            {
                bool parkingtype = Enum.TryParse<Parkingtype>(Info.ParkingType, true, out Parkingtype Parkingtype);

                string VehicalNo = Info.VehicalNo;
                var Validation = dBContext.Entities.Where(u => u.VehicalNo == VehicalNo).FirstOrDefault();

                Info.ParkingSlotNo = AllotcateSlot();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     //throw exception when user exist
                }
                Info.ParkStatus = true;
                var Result = dBContext.Entities.Add(Info);
               
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
            
            bool flag = true;
            while (flag != false)
            {
                slot = (random.Next() % 100) + 1;
                var result = dBContext.Entities.Where(u => u.ParkingSlotNo == slot).FirstOrDefault();
                if (result == null)
                {
                    flag = false;                     
                }
            }
            return slot;
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
                var Entries = (from x in dBContext.Entities
                               where x.VehicalNo == Info.VehicalNo
                               select x).First();

                if (Entries.ParkStatus == false)
                {
                    throw new Exception("Car is Unparked Already");
                }

                if (Entries != null)
                {
                    Entries.ParkingSlotNo = 0;
                    Entries.ExitTime = DateTime.Now;
                    int timeDiff = Entries.ExitTime.Subtract(Entries.EntryTime).Hours;
                    Entries.ChargePerHr = timeDiff * 10;
                    Entries.ParkStatus = false
                        ;
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
        /// Giving the All in form pf list
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllParkingData()
        {
            var allEntries = dBContext.Entities.ToList();

            return allEntries;
        }
        /// <summary>
        /// Giving the All in form pf list
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllParkData()
        {
            var allEntries = dBContext.Entities.Where(x => x.ParkStatus == true).ToList();

            return allEntries;
        }
        /// <summary>
        /// Giving the All in form pf list
        /// </summary>
        /// <returns></returns>
        public List<ParkingInformation> GetAllUnParkData()
        {
            var allEntries = dBContext.Entities.Where(x => x.ParkStatus == false).ToList();

            return allEntries;
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
                var details = dBContext.Entities.First(x => x.ParkingID == ReceiptNumber);

                if (details != null)
                {
                    dBContext.Entities.Remove(details);

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
                var Validation = dBContext.Entities.Where(u => u.VehicalNo == VehicalNo && u.ParkingID != ID).FirstOrDefault();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     
                }

                var Entries = (from x in dBContext.Entities
                               where x.ParkingID == ID
                               select x).First();
                if (Entries != null)
                {
                    Entries.ParkingSlotNo = Info.ParkingSlotNo;
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
                if (dBContext.Entities.Any(x => x.VehicalNo == VehicleNo))
                {
                    return (from Details in dBContext.Entities
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
                if (dBContext.Entities.Any(x => x.VehicalBrand == brand))
                {

                    var VehicleData = (from Details in dBContext.Entities
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
