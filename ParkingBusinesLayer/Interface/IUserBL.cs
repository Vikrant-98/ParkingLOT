using ParkingCommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingBusinesLayer.Interface
{
    public interface IUserBL
    {
        //Registration
        bool AddUser(Users Info);
        //Login
        bool LoginVerification(Login Info);

        object DeleteUserDetails(int ReceiptNumber);

        object UpdateUserRecord(Users Info, int ID);

    }
}
