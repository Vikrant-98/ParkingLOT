using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ParkingBusinesLayer.Interface;
using ParkingCommonLayer.Services;
using ParkingReposLayer.ApplicationDB;

namespace Parking_LOT.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        readonly IParkingBL _BusinessLayer;
        private readonly IConfiguration _configuration;
        private Application dBContext;

        public ParkingController(IParkingBL _BusinessDependencyInjection, IConfiguration _configuration, Application dBContext)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
            this.dBContext = dBContext;
        }
        //
        /// <summary>
        /// Park the vehicle
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        [Route("ParkVehicle")]
        [HttpPost]
        public IActionResult ParkVehicle([FromBody]Information Info)
        {
            try
            {
                bool data = _BusinessLayer.ParkVehicle(Info);                 //data return indexer SMD format

                if (data == true)
                {
                    var status = true;
                    var Message = "ParkVehicle successful ";
                    return Ok(new { status, Message, Info });                  //SMD for Login Success 
                }
                else
                {
                    var status = false;
                    var Message = "Parking Failed";
                    return BadRequest(new { status, Message });                             //SMD for Login Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Parking Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        /// <summary>
        /// Unpark the Vehicle
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        [Route("UnparkVehicle")]
        [HttpPost]
        public IActionResult UnPark_Vehicle([FromBody]Unpark Info)
        {
            try
            {
                bool data = _BusinessLayer.UnparkVehicle(Info);                 //data return indexer SMD format

                if (data == true)
                {
                    var status = true;
                    var Message = "Unparking successful ";
                    return Ok(new { status, Message, Info });                  //SMD for Login Success 
                }
                else
                {
                    var status = false;
                    var Message = "Unparking Failed";
                    return BadRequest(new { status, Message });                             //SMD for Ligin Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Unparking Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        /// <summary>
        /// Get All details
        /// </summary>
        /// <returns></returns>
        [Route("ParkingDetails")]
        [HttpGet]
        [Authorize(Roles = "Owner,Police")]
        public ActionResult GetAllParkingData()
        {                                    
            try
            {
                var data = _BusinessLayer.GetAllParkingData();                                  //SMD for All Details
                if (data != null)
                {
                    bool status = true;
                    string message = "ALL Records of Parking ";
                    return Ok(new { status, message, data });                                   //SMD for All Details
                }
                else
                {
                    var status = false;
                    var Message = "Failed";
                    return BadRequest(new { status, Message });                             //SMD for Ligin Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        /// <summary>
        /// Get All details
        /// </summary>
        /// <returns></returns>
        [Route("parkDetails")]
        [HttpGet]
        [Authorize(Roles = "Owner,Police")]
        public ActionResult GetAllParkVehicleData()
        {
            try
            {
                var data = _BusinessLayer.GetAllParkedData();
                if (data != null)
                {
                    bool status = true;
                    string message = "ALL Records of Park Vehicle ";
                    return Ok(new { status, message, data });                                   //SMD for All Details
                }
                else
                {
                    var status = false;
                    var Message = "Failed";
                    return BadRequest(new { status, Message });                             //SMD for Ligin Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }

        }
        /// <summary>
        /// Get All details
        /// </summary>
        /// <returns></returns>
        [Route("UnparkDetails")]
        [HttpGet]
        [Authorize(Roles = "Owner,Police")]
        public ActionResult GetAllUnParkVehicleData()
        {
            try
            {
                var data = _BusinessLayer.GetAllUnParkedData();                                  //SMD for All Details
                if (data != null)
                {
                    bool status = true;
                    string message = "ALL Records of UnPark Vehicle ";
                    return Ok(new { status, message, data });                                     //SMD for All Details
                }
                else
                {
                    var status = false;
                    var Message = "Failed";
                    return BadRequest(new { status, Message });                             //SMD for Ligin Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        /// <summary>
        /// Delete Record for Vehicle
        /// Authorizer by Owner
        /// </summary>
        /// <param name="ReceiptNumber"></param>
        /// <returns></returns>
        [Authorize(Roles = "Owner")]
        [HttpDelete]
        [Route("{ReceiptNumber}")]
        public ActionResult DeleteCarParkingDetails(int ReceiptNumber)
        {
            try
            {
                var data = _BusinessLayer.DeleteCarParkingDetails(ReceiptNumber);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Delete";
                    return Ok(new { success, message, data });                          //SMD format for Delete succesful

                }
                else
                {
                    success = false;
                    message = "Fail To Delete";
                    return BadRequest(new { success, message });                        //SMD format for Delete unsuccess

                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }
        /// <summary>
        /// Update Record
        /// Authorizer by Owner
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("{ID}")]
        [HttpPut]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateRecord([FromBody]Information Info,int ID)
        {
            try
            {

                var data = _BusinessLayer.UpdateParkingRecord(Info,ID);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Updated Successfull";
                    return Ok(new
                    {
                        status,
                        Message,
                        data
                    });                                                             //data return indexer SMD format when Register success
                }
                else
                {
                    var status = false;
                    var Message = "Not Updated Succesfully";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Not Updated Succesfully";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }

        /// <summary>
        /// Get car details from vehicle number
        /// Authorizer by Owner and police
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("VehicleNumber")]
        public ActionResult GetCarDetailsByVehicleNumber([FromBody]Unpark Info)
        {
            try
            {
                var data = _BusinessLayer.GetCarDetailsByVehicleNumber(Info.VehicalNo);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Search ";
                    return Ok(new { success, message, data });                          //SMD format for Vehicle Number Details
                    
                }
                else
                {
                    success = false;
                    message = "Fail To Search";
                    return Ok(new { success, message });                                //SMD format for not get any Details
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }
        /// <summary>
        /// Get car details by their brand
        /// Authorizer by Owner and police
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("{brand}")]
        public ActionResult GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                var data = _BusinessLayer.GetCarDetailsByVehicleBrand(brand);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Search";
                    return Ok(new { success, message, data });                          //SMD format for Vehicle brand found
                }
                else
                {
                    success = false;
                    message = "Search Fail";
                    return Ok(new { success, message });                                //SMD format for vehicle not found
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }
    }
}