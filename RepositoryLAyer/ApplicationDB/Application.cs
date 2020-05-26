using CommonLayer.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLAyer.ApplicationDB
{
    public class Application : DbContext
    {
        public Application(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParkingUser> Users { get; set; }
        
    }

}  

