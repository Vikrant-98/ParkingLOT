using ParkingCommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingReposLayer.Interface
{
    public interface IUserRL
    {
        //Registration
        bool AddUser(ParkingUser Info);
        //Login
        bool LoginVerification(Login Info);

        object DeleteUserDetails(int ReceiptNumber);

        object UpdateUserRecord(Users Info, int ID);


    }
}
