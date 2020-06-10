using ParkingBusinesLayer.Interface;
using ParkingCommonLayer.Services;
using ParkingReposLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingBusinesLayer.Service
{
    public class UserBL : IUserBL
    {
        private IUserRL User;

        public UserBL(IUserRL Data)
        {
            this.User = Data;
        }

        /// <summary>
        /// Add Information to Register the user 
        /// Return the status true ro false
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool AddUser(Users Info)
        {
            try
            {
                string Encrypted = Info.Password;
                Info.Password = EncryptedPassword.EncodePasswordToBase64(Encrypted);            //Password get Encrypted
                var Result = User.AddUser(Info);
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
                var Result = User.LoginVerification(Info);                               //get result true or false
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

        public object DeleteUserDetails(int ReceiptNumber)
        {
            try
            {
                var data = User.DeleteUserDetails(ReceiptNumber);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);

            }
        }
        public object UpdateUserRecord(Users Info, int ID)
        {
            try
            {
                var Result = User.UpdateUserRecord(Info, ID);
                if (Result != null)
                {
                    return Result;
                }
                else
                {
                    throw new Exception("Record not updated!!!!!!!!!!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
