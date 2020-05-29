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
        readonly IParkingBL _businessLayer;
        readonly IParkingRL _reposLayer;
        private readonly IConfiguration _configuration;

        public static DbContextOptions<Application> User { get; }

        public static string sqlconnectionstring = "Data Source=DESKTOP-MQSNJSU;Initial Catalog=ParkingDB;Integrated Security=True";

        static UnitTest1()
        {
            User = new DbContextOptionsBuilder<Application>().UseSqlServer(sqlconnectionstring).Options;
        }

        public UnitTest1()
        {

            var context = new Application(User);
            _reposLayer = new ParkingRL(context);
            _businessLayer = new ParkingBL(_reposLayer);
        }
        [Fact]
        public void GivenDataPassResult_Ok()
        {
            var controller = new ParkingController(_businessLayer,_configuration);
            var result = new ParkingUser
            {
                FirstName = "Aarti",
                LastName = "Ahire",
                MailID = "aartiahire@gmail.com",
                DriverCategory = "Police",
                Password = "aartiahire"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GivenIncorrectDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new ParkingUser
            {
                FirstName = "Vinayak",
                LastName = "UshaKola",
                MailID = "vinayak@gmail.com",
                Password = "vinayak@5678"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void GivenIncorrectNameFormatPass_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new ParkingUser
            {
                FirstName = "vikrant",
                LastName = "chitte",
                MailID = "vikrantchitte@gmail.com",
                Password = "vikrantchitte"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }

        [Fact]
        public void GivenIncorrect_EMailDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new ParkingUser
            {
                FirstName = "Vinayak",
                LastName = "UshaKola",
                MailID = "v@gmail.com",
                DriverCategory = "Owner",
                Password = "vinayak@5678"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void Given_Existing_EMailDataFormatPass_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new ParkingUser
            {
                FirstName = "Vinayak",
                LastName = "UshaKola",
                MailID = "vinayak@gmail.com",
                DriverCategory = "Owner",
                Password = "Vikayak1234"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void GivenSameDataPasses_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new ParkingUser
            {
                FirstName = "Vishal",
                LastName = "Chitte",
                MailID = "vishalchitte@gmail.com",
                DriverCategory = "Driver",
                Password = "vishal@6666"
            };
            var okResult = controller.RegisterUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void GivenInvalidValid_EmailandPassword_Passes_BadRequestResult_Exception()
        {
            var controller = new ParkingController(_businessLayer, _configuration);
            var result = new Login
            {
                MailID = "vishalchitte@gmail.com",
                Password = "vishal@1234"
            };
            var okResult = controller.LoginUser(result);

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
    }
}
