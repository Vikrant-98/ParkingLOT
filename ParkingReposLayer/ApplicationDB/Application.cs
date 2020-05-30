using Microsoft.EntityFrameworkCore;
using ParkingCommonLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingReposLayer.ApplicationDB
{
    public class Application : DbContext
    {
        /// <summary>
        /// Creating the database for the User
        /// </summary>
        /// <param name="options"></param>
        public Application(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParkingUser> Users { get; set; }

        public DbSet<ParkingInformation> Entities { get; set; }

    }
}
