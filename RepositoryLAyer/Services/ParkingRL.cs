using CommonLayer.Services;
using RepositoryLAyer.ApplicationDB;
using RepositoryLAyer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLAyer.Services
{
    public class ParkingRL : IParkingRL
    {
        private Application dBContext;

        public ParkingRL(Application dBContext)
        {
            this.dBContext = dBContext;
        }
        public bool ParkingLoginDatails(ParkingUser Info)
        {
            try
            {
                var Result = dBContext.Logins.Add(Info);
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
    }
}
