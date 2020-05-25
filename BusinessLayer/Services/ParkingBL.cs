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
        public bool ParkingLoginDatails(ParkingUser Info)
        {
            try
            {
                var Result = Parking.ParkingLoginDatails(Info);
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
    }
}
