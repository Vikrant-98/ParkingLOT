using CommonLayer.Services;
using RepositoryLAyer.ApplicationDB;
using RepositoryLAyer.Interface;
using System;
using System.Linq;
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
        public bool ParkingRegisterDatails(ParkingUser Info)
        {
            try
            {
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
        public bool ParkingLoginDatails(Login Info)
        {
            try
            {
                string MailID = Info.MailID;
                string Password = EncryptedPassword.EncodePasswordToBase64(Info.Password);
                
                var Result = dBContext.Users.Where(u => u.MailID == MailID && u.Password == Password).FirstOrDefault();

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
