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

        public ParkingRL(Application dBContext)
        {
            this.dBContext = dBContext;
        }
        /// <summary>
        /// Registration for new User 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool AddUser(ParkingUser Info)
        {
            try
            {
                bool input = Enum.TryParse<Driver>(Info.DriverCategory, true, out Driver driver);
                if (input != true)
                {
                    throw new Exception("Invalid Driver Category");
                }
                string MailID = Info.MailID;
                //Validation for unique MailID
                var Validation = dBContext.Users.Where(u => u.MailID == MailID).FirstOrDefault();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     //throw exception when user exist
                }

                var Result = dBContext.Users.Add(Info);
                dBContext.SaveChanges();
                if (Result != null)
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
        /// Checking for valid user using MailID and Password
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool LoginVerification(Login Info)
        {
            try
            {
                bool input = Enum.TryParse<Driver>(Info.DriverCategory, true, out Driver driver);
                if (input != true)
                {
                    throw new Exception("Invalid Driver Category");
                }
                string MailID = Info.MailID;
                string Password = EncryptedPassword.EncodePasswordToBase64(Info.Password);          //Password Encrypted
                string DriverCategory = Info.DriverCategory;

                var Result = dBContext.Users.Where(u => u.MailID == MailID && u.Password == Password && u.DriverCategory == DriverCategory).FirstOrDefault();

                if (Result != null)
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
        public bool ParkVehicle(ParkingInformation Info)
        {
            try
            {
                bool parkingtype = Enum.TryParse<Parkingtype>(Info.ParkingType, true, out Parkingtype Parkingtype);

                string VehicalNo = Info.VehicalNo;
                //Validation for unique MailID
                var Validation = dBContext.Entities.Where(u => u.VehicalNo == VehicalNo).FirstOrDefault();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");                     //throw exception when user exist
                }

                var Result = dBContext.Entities.Add(Info);
                dBContext.SaveChanges();
                if (Result != null)
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
                var Entries = (from x in dBContext.Entities
                               where x.VehicalNo == Info.VehicalNo
                               select x).First();

                if (Entries.ParkStatus == true)
                {
                    throw new Exception("Car is Unparked Already");
                }

                if (Entries != null)
                {
                    Entries.ParkingSlotNo = 0;
                    Entries.ExitTime = DateTime.Now;
                    int timeDiff = Entries.ExitTime.Subtract(Entries.EntryTime).Hours;
                    Entries.ChargePerHr = timeDiff * 10;
                    Entries.ParkStatus = true;
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
        public List<ParkingInformation> GetAllParkingData()
        {
            var allEntries = dBContext.Entities.ToList();

            return allEntries;
        }
        public object DeleteCarParkingDetails(int ReceiptNumber)
        {
            try
            {
                var details = dBContext.Entities.First(x => x.ParkingID == ReceiptNumber);

                if (details != null)
                {
                    dBContext.Entities.Remove(details);

                    dBContext.SaveChanges();
                    return "Data Deleted Successfully";
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
        public bool UpdateRecord(ParkingInformation Info, int ID)
        {
            try
            {
                string VehicalNo = Info.VehicalNo;
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
                    Entries.VehicalNo = Info.VehicalNo;
                    Entries.VehicalColor = Info.VehicalColor;
                    Entries.ExitTime = Info.ExitTime;
                    Entries.ParkStatus = Info.ParkStatus;
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
