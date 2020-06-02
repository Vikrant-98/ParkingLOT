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
        public bool Addparking(ParkingUser Info)
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
                var Validation = dBContext.Users.Where(u => u.MailID == MailID ).FirstOrDefault();

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
        public List<ParkingInformation> GetAllParkingData()
        {
            var allEntries = dBContext.Entities.Select(x => x).ToList();

            return allEntries;
        }
        public bool DeleteRecord(string VehicleNo)
        {
            try
            {
                var Entries = dBContext.Entities.First(x => x.VehicalNo == VehicleNo);

                dBContext.Entities.Remove(Entries);
                dBContext.SaveChanges();
                if (Entries != null)
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
        public bool UpdateRecord(ParkingInformation Info)
        {
            try
            {
                var Entries = (from x in dBContext.Entities
                              where x.ParkingID == Info.ParkingID
                              select x).First();
                if (Entries != null)
                {
                    Entries.ParkingSlotNo = Info.ParkingSlotNo;
                    Entries.ParkingType = Info.ParkingType;
                    Entries.VehicalBrand = Info.VehicalBrand;
                    Entries.VehicalNo = Info.VehicalNo;
                    Entries.VehicalColor = Info.VehicalColor;
                    Entries.EntryTime = Info.EntryTime;
                    Entries.ExitTime = Info.ExitTime;
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
    }
}
