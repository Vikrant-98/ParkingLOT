using BusinessLayer.Interface;
using CommonLayer.Services;
using RepositoryLAyer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ParkingBL : IParkingBL
    {
        private IParkingRL Parking;
        public ParkingBL(IParkingRL data)
        {
            Parking = data;
        }
        public bool ParkingRegisterDatails(ParkingUser Info)
        {
            try
            {
                string Encrypted = Info.Password;
                Info.Password = EncryptedPassword.EncodePasswordToBase64(Encrypted);
                var Result = Parking.ParkingRegisterDatails(Info);
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
        public bool ParkingLoginDatails(Login Info)
        {
            try
            {
                var Result = Parking.ParkingLoginDatails(Info);
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
