using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Parking_LOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IConfiguration configuration;
        IParkingBL BusinessLayer;

        public ParkingController(IParkingBL BusinessDependencyInjection, IConfiguration configuration)
        {
            BusinessLayer = BusinessDependencyInjection;
            this.configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult ParkingLOTDetails([FromBody]ParkingUser Info)
        {
            try
            {
                bool data = BusinessLayer.ParkingLoginDatails(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Register Succesfull";
                    return this.Ok(new { status, Message });
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
                return BadRequest(new { error = e.Message });
            }
        }
    }
}