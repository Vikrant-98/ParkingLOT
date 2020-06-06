﻿using System;
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
using ParkingReposLayer.ApplicationDB;

namespace Parking_LOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Dependenct Injection from Business layer 
        /// </summary>
        readonly IUserBL _BusinessLayer;
        private readonly IConfiguration _configuration;
        private Application dBContext;
        
        public UserController(IUserBL _BusinessDependencyInjection, IConfiguration _configuration, Application dBContext)
        {
            _BusinessLayer = _BusinessDependencyInjection;
            this._configuration = _configuration;
            this.dBContext = dBContext;
        }
       /// <summary>
       /// User get registered
       /// </summary>
       /// <param name="Info"></param>
       /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody]ParkingUser Info)
        {
            try
            {
                
                bool data = _BusinessLayer.AddUser(Info);                    //accept the result form Repos layer
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
        /// <summary>
        /// User login
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
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
                    new Claim("MailID", Info.MailID.ToString()),
                    new Claim("Password", Info.Password.ToString())
                };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                     _configuration["Jwt:Issuer"],
                     claims,
                     expires: DateTime.Now.AddHours(1),
                     signingCredentials: signingCreds);                                   //Token Generate for User Category
                
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
        /// <summary>
        /// Delete User Details
        /// Authorize by owner
        /// </summary>
        /// <param name="ReceiptNumber"></param>
        /// <returns></returns>
        [Authorize(Roles = "Owner")]
        [HttpDelete]
        [Route("{ReceiptNumber}")]
        public ActionResult DeleteUserDetails(int ReceiptNumber)
        {
            try
            {
                var data = _BusinessLayer.DeleteUserDetails(ReceiptNumber);
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
        /// <summary>
        /// Update user record
        /// Authorize by owner
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("{ID}")]
        [HttpPut]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateUserRecord([FromBody]Users Info, int ID)
        {
            try
            {

                var data = _BusinessLayer.UpdateUserRecord(Info,ID);                    //accept the result form Repos layer
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
    }
}