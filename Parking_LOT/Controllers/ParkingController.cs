using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingBusinesLayer.Interface;
using ParkingCommonLayer.Services;

namespace Parking_LOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        /// <summary>
        /// Dependenct Injection from Business layer 
        /// </summary>
        readonly IParkingBL _BusinessLayer;
        private readonly IConfiguration _configuration;

        public ParkingController(IParkingBL _BusinessDependencyInjection, IConfiguration _configuration)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
        }
       
        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody]ParkingUser Info)
        {
            try
            {
                
                bool data = _BusinessLayer.Addparking(Info);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Register Successfull";
                    return Ok(new {
                        status,
                        Message,
                        Info.ID,
                        Info.FirstName,
                        Info.LastName,
                        Info.MailID,
                        Info.DriverCategory,
                        Info.CreateDate,
                        Info.ModifiedDate
                    });                                                             //data return indexer SMD format when Register success
                }
                else
                {
                    var status = false;
                    var Message = "Register Failed";
                    return this.BadRequest(new { status, Message});
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Register Failed";
                return BadRequest(new { status , error = e.Message , Message });
            }
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult LoginUser([FromBody]Login Info)
        {
            try
            {

                bool data = _BusinessLayer.LoginVerification(Info);                 //data return indexer SMD format
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Info.DriverCategory.ToString()),
                    new Claim("UserName", Info.MailID.ToString()),
                    new Claim("Password", Info.Password.ToString())
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(120),
                    signingCredentials: signingCreds);                                      //Token Generate for User Category

                if (data == true)
                {
                    var status = true;
                    var Message = "Login successful ";
                    var Token = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { status,Info.MailID, Message, Token });                  //SMD for Login Success 
                }
                else
                {
                    var status = false;
                    var Message = "Login Failed";
                    return BadRequest(new { status, Message });                             //SMD for Ligin Fails
                }
            }
            catch (Exception e)
            {
                var status = false;
                var Message = "Login Failed";
                return BadRequest(new { status , error = e.Message , Message });
            }
        }
        [Route("Driver")]
        [HttpPost]
        public IActionResult Driver([FromBody]ParkingUser Info)
        {
            try
            {

                bool data = _BusinessLayer.Addparking(Info);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Register Successfull";
                    return Ok(new
                    {
                        status,
                        Message,
                        Info.ID,
                        Info.FirstName,
                        Info.LastName,
                        Info.MailID,
                        Info.DriverCategory,
                        Info.CreateDate,
                        Info.ModifiedDate
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
        //[HttpGet]
        //[Authorize(Roles = "Owner,Police")]
        //public ActionResult<List<ParkingUser>> AllUserData()
        //{
        //    bool success = true;
        //    string message = "Successfully Added Record Data";
        //    var data = userBL.AllUserData();
        //    return Ok(new { success, message, data });
        //    //return a;
        //}
    }
}