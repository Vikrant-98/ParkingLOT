using ParkingCommonLayer.Services;
using ParkingReposLayer.ApplicationDB;
using ParkingReposLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingReposLayer.Services
{
    public class UserRL : IUserRL
    {
        private Application dBContext;

        public UserRL(Application dBContext)
        {
            this.dBContext = dBContext;
        }

        /// <summary>
        /// Registration for new User 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool AddUser(Users Info)
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
                ParkingUser data = new ParkingUser
                {
                    FirstName = Info.FirstName,
                    LastName = Info.LastName,
                    MailID = Info.MailID,
                    DriverCategory = Info.DriverCategory,
                    Password = Info.Password
                };
                var Result = dBContext.Users.Add(data);
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

        public object DeleteUserDetails(int ReceiptNumber)
        {
            try
            {
                var details = dBContext.Users.First(x => x.ID == ReceiptNumber);

                if (details != null)
                {
                    dBContext.Users.Remove(details);

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
        
        public object UpdateUserRecord(Users Info, int ID)
        {
            try
            {
                string MailID = Info.MailID;
                string Password = EncryptedPassword.EncodePasswordToBase64(Info.Password);
                var Validation = dBContext.Users.Where(u => u.MailID == MailID && u.ID != ID).FirstOrDefault();

                if (Validation != null)
                {
                    throw new Exception("User Already Exist ");
                }

                var Entries = (from x in dBContext.Users
                               where x.ID == ID
                               select x).First();
                if (Entries != null)
                {
                    Entries.FirstName = Info.FirstName;
                    Entries.LastName = Info.LastName;
                    Entries.MailID = Info.MailID;
                    Entries.Password = Password;
                    Entries.DriverCategory = Info.DriverCategory;
                    Entries.ModifiedDate = DateTime.Now;
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
    }
}
