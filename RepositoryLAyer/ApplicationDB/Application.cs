using CommonLayer.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLAyer.ApplicationDB
{
    public class Application : DbContext
    {
        public Application(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParkingUser> Logins { get; set; }

    }
}
