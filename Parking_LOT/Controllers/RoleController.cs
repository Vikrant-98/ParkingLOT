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
    public class RoleController : ControllerBase
    {
        readonly IParkingBL _BusinessLayer;
        private readonly IConfiguration _configuration;
        private Application dBContext;

        public RoleController(IParkingBL _BusinessDependencyInjection, IConfiguration _configuration, Application dBContext)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
            this.dBContext = dBContext;
        }

        [Route("AllDetails")]
        [HttpGet]
        [Authorize(Roles = "Owner,Police")]
        public ActionResult<List<ParkingInformation>> GetAllParkingData()
        {
            bool success = true;
            string message = "ALL Records of Parking ";
            var data = _BusinessLayer.GetAllParkingData();
            return Ok(new { success, message, data });
        }

        //[Authorize(Roles = "Admin")]
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
                    return Ok(new { success, message, data });

                }
                else
                {
                    success = false;
                    message = "Fail To Delete";
                    return Ok(new { success, message });

                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        [Route("UpdateRecord")]
        [HttpPatch]
        //[Authorize(Roles = "Owner")]
        public IActionResult UpdateRecord([FromBody]ParkingInformation Info,int ID)
        {
            try
            {

                bool data = _BusinessLayer.UpdateRecord(Info,ID);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Updated Successfull";
                    return Ok(new
                    {
                        status,
                        Message,
                        Info
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

        //[Authorize(Roles = "Owner,Police")]
        [HttpGet]
        [Route("Search/{vehicleNumber}")]
        public ActionResult GetCarDetailsByVehicleNumber(string vehicleNumber)
        {
            try
            {
                var data = _BusinessLayer.GetCarDetailsByVehicleNumber(vehicleNumber);
                bool success = false;
                string message;
                if (data != null)
                {
                    success = true;
                    message = "Search ";
                    return Ok(new { success, message, data });
                    
                }
                else
                {
                    success = false;
                    message = "Fail To Search";
                    return Ok(new { success, message });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }
        [HttpPost]
        [Route("Search/{brand}")]
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
                    return Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Search Fail";
                    return Ok(new { success, message });
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