using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Parking_LOT.Controllers;
using ParkingBusinesLayer.Interface;
using ParkingBusinesLayer.Service;
using ParkingCommonLayer.Services;
using ParkingReposLayer.ApplicationDB;
using ParkingReposLayer.Interface;
using ParkingReposLayer.Services;
using System;
using Xunit;

namespace XUnitTestforParkingLOT
{
    public class UnitTest1
    {
        readonly IParkingBL _businessLayerparking;
        readonly IParkingRL _reposLayerParking;
        readonly IUserRL _reposLayeruser;
        readonly IUserBL _businessLayeruser;
        private readonly IConfiguration _configuration;
        private Application dBContext;

        public static DbContextOptions<Application> User { get; }

        public static string sqlconnectionstring = "Data Source=DESKTOP-MQSNJSU;Initial Catalog=ParkingDB;Integrated Security=True";

        static UnitTest1()
        {
            User = new DbContextOptionsBuilder<Application>().UseSqlServer(sqlconnectionstring).Options;
        }

        public UnitTest1()
        {

            var context = new Application(User);
            _reposLayeruser = new UserRL(context);
            _reposLayerParking = new ParkingRL(context);
            _businessLayeruser = new UserBL(_reposLayeruser);
            _businessLayerparking = new ParkingBL(_reposLayerParking);
        }
        /// <summary>
        /// Given all details pass ok
        /// </summary>
        [Fact]
        public void GivenDataPassResult_Ok()
        {
            var controller = new UserController(_businessLayeruser,_configuration,dBContext);
            var result = new Users
            {
                FirstName = "Kartika",
                LastName = "Sharma",
                MailID = "kartikasharma@gmail.com",
                DriverCategory = "Security",
                Password = "kartika1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Driver Cahegory missing
        /// </summary>
        [Fact]
        public void GivenIncorrectDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Users
            {
                FirstName = "Vinayak",
                LastName = "UshaKola",
                MailID = "vinayak@gmail.com",
                Password = "vinayak1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Incorrect nake format pass bad request Exception
        /// </summary>
        [Fact]
        public void GivenIncorrectNameFormatPass_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Users
            {
                FirstName = "vikrant",
                LastName = "chitte",
                MailID = "vikrantchitte@gmail.com",
                Password = "vikrantchitte"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Incorrect Email Address Exception
        /// </summary>
        [Fact]
        public void GivenIncorrect_EMailDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Users
            {
                FirstName = "Vinayak",
                LastName = "UshaKola",
                MailID = "v@gmail.com",
                DriverCategory = "Owner",
                Password = "vinayak1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Existing Email Address Exception
        /// </summary>
        [Fact]
        public void Given_Existing_EMailDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Users
            {
                FirstName = "Vikrant",
                LastName = "Chitte",
                MailID = "chittevikrant@gmail.com",
                DriverCategory = "Owner",
                Password = "Vikrant1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Same Data Passing Bad request Exception 
        /// </summary>
        [Fact]
        public void GivenSameDataPasses_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Users
            {
                FirstName = "Vishal",
                LastName = "Chitte",
                MailID = "vishalchitte@gmail.com",
                DriverCategory = "Driver",
                Password = "vishal1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Given wrong Email address and password Exception
        /// </summary>
        [Fact]
        public void GivenInvalidValid_EmailandPassword_Passes_BadRequestResult_Exception()
        {
            var controller = new UserController(_businessLayeruser, _configuration,dBContext);
            var result = new Login
            {
                MailID = "vishalchitte@gmail.com",
                Password = "vishal@1234"
            };
            var okResult = controller.LoginUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Given Correct Data for Add Parking result OK  
        /// </summary>
        [Fact]
        public void GivenDataForParkedVehiclePasses_Ok()
        {
            var controller = new ParkingController(_businessLayerparking, _configuration, dBContext);
            var result = new Information
            {
                VehicalNumber ="MH 15 YD 8951",
                VehicalBrand="BMW",
                VehicalColor="White",
                ParkingType="Vallet"
            };
            var okResult = controller.ParkVehicle(result);

            Assert.IsType<OkObjectResult>(okResult);
        }
        /// <summary>
        /// Passing Same Data Result Bad request Exception 
        /// </summary>
        [Fact]
        public void GivenSameForParkedVehicleDataPasses_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayerparking, _configuration, dBContext);
            var result = new Information
            {
                VehicalNumber = "MH 07 BT 1895",
                VehicalBrand = "BMW",
                VehicalColor = "Black",
                ParkingType = "Owner"
            };
            var okResult = controller.ParkVehicle(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        /// <summary>
        /// Given Incorrect Vehicle Number passes bad request Exception
        /// </summary>
        [Fact]
        public void GivenWrongParkedVehicleNumberPasses_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayerparking, _configuration, dBContext);
            var result = new Information
            {
                VehicalNumber = "MH 07",
                VehicalBrand = "BMW",
                VehicalColor = "Black",
                ParkingType = "Owner"
            };
            var okResult = controller.ParkVehicle(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
    }
}
