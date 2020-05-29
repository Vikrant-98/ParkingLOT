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
        /// Add Information to Register the user 
        /// Return the status true ro false
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool Addparking(ParkingUser Info)
        {
            try
            {
                string Encrypted = Info.Password;
                Info.Password = EncryptedPassword.EncodePasswordToBase64(Encrypted);            //Password get Encrypted
                var Result = Parking.Addparking(Info);
                if (!Result.Equals(null))
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
        /// Information for verification of Mail ID and Password
        /// Return the status true ro false
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool LoginVerification(Login Info)
        {
            try
            {
                var Result = Parking.LoginVerification(Info);                               //get result true or false
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
    }
}
