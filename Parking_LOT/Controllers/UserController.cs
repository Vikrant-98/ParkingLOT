using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using ParkingBusinesLayer.Interface;
using ParkingCommonLayer.ResponseModels;
using ParkingCommonLayer.Services;
using ParkingReposLayer.ApplicationDB;
using Parking_LOT.MSMQService;

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
        MessageSender msmqSender = new MessageSender();

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
        public IActionResult RegisterUser([FromBody]Users Info)
        {
            try
            {
                string password = Info.Password;
                bool data = _BusinessLayer.AddUser(Info);                    //accept the result form Repos layer
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Register Successfull";
                    RegistrationResponse responseData = new RegistrationResponse
                    {
                       FirstName = Info.FirstName,
                       LastName = Info.LastName,
                       MailID = Info.MailID,
                       DriverCategory = Info.DriverCategory
                    };
                    string messagesender = "Registration successful with" + "\n Email : " + Convert.ToString(Info.MailID) + "\n and" + "\n Password : " + Convert.ToString(password) + "\n of User type : " + Info.DriverCategory;
                    msmqSender.Message(messagesender);
                    return Ok(new {
                        status,
                        Message,
                        responseData
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
        private void Mail()
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Vikrant", "chittevikey55@gmail.com"));
            message.To.Add(new MailboxAddress("Vikrant", "chittevikey5@gmail.com"));
            message.Subject = "Sample Mail";
            message.Body = new TextPart()
            {
                Text = "This is Sample mail"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("chittevikey55@gmail.com","vik98CH!");
                client.Send(message);
                client.Disconnect(true);
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
                var jsontoken = GenerateToken(Info);
                if (data == true)
                {
                    LoginResponse responseData = new LoginResponse
                    {
                        mail = Info.MailID,
                        token = jsontoken
                    };
                    var status = true;
                    var Message = "Login successful ";
                    
                    return Ok(new { status, Message, responseData});                  //SMD for Login Success 
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
        /// Generates Token for Login
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private string GenerateToken(Login Info)
        {
            try
            {
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
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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