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
                string MailID = Info.MailID;
                string Password = EncryptedPassword.EncodePasswordToBase64(Info.Password);          //Password Encrypted
                Driver DriverCategory = Info.DriverCategory;
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
    }
}
