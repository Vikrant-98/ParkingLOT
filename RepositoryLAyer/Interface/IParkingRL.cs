﻿using CommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLAyer.Interface
{
    public interface IParkingRL
    {
        bool ParkingRegisterDatails(ParkingUser Info);

        bool ParkingLoginDatails(Login Info);
    }
}
