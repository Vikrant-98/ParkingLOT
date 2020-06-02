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

namespace Parking_LOT.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly IParkingBL _BusinessLayer;
        private readonly IConfiguration _configuration;

        public RoleController(IParkingBL _BusinessDependencyInjection, IConfiguration _configuration)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
        }

        [Route("Driver")]
        [HttpPost]
        public IActionResult Owner([FromBody]ParkingInformation Info)
        {
            try
            {

                bool data = _BusinessLayer.ParkVehicle(Info);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Register Successfull";
                    return Ok(new
                    {
                        status,
                        Message

                    });                                                             //data return indexer SMD format when Register success
                }
                else
                {
                    var status = false;
                    var Message = "Register Failed";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Register Failed";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        [Route("AllDetails")]
        [HttpGet]
        [Authorize(Roles = "Owner,Police")]
        public ActionResult<List<ParkingInformation>> GetAllParkingData()
        {
            bool success = true;
            string message = "Successfully Added Record Data";
            var data = _BusinessLayer.GetAllParkingData();
            return Ok(new { success, message, data });
        }
        [Route("DeleteRecord")]
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult DeleteRecord([FromBody]ParkingInformation Info)
        {
            try
            {

                bool data = _BusinessLayer.DeleteRecord(Info.VehicalNo);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Record Delete Successfull";
                    return Ok(new
                    {
                        status,
                        Message

                    });                                                             //data return indexer SMD format when Register success
                }
                else
                {
                    var status = false;
                    var Message = "Record is Not Deleted";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Record is Not Deleted";
                return BadRequest(new { status, error = e.Message, Message });
            }
        }
        [Route("UpdateRecord")]
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateRecord([FromBody]ParkingInformation Info)
        {
            try
            {

                bool data = _BusinessLayer.UpdateRecord(Info);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Updated Successfull";
                    return Ok(new
                    {
                        status,
                        Message

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
}