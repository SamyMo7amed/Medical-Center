using AutoMapper;
using Castle.Core.Smtp;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly  IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Medical_CenterAPI.Service.IEmailSender emailSender;
        
        public AccountController(IUnitOfWork unitOfWork,IMapper mapper, Medical_CenterAPI.Service.IEmailSender emailSender) {
            this.emailSender = emailSender; 
         this.unitOfWork = unitOfWork;                  
        this.mapper = mapper;
            
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUser registerUser)
         {
            if (ModelState.IsValid) {

                var User = mapper.Map<Patient>(registerUser);   
                var check= await unitOfWork.UserManager.FindByEmailAsync(registerUser.Email); 
                if(check != null)
                {
                    ModelState.AddModelError("", "The email already exists.");
                    return BadRequest(ModelState);
                }
                else { 
                    var result= await unitOfWork.UserManager.CreateAsync(User,User.Password);
                    if (result.Succeeded)
                    {
                       

                        return Ok(new { Message = "User registered successfully" });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
            
             
            
           
            
            
            }

            return  BadRequest(ModelState);

        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> SaveRegister(RegisterUser UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                var user=mapper.Map<Patient>(UserFromRequest);
                var result= await unitOfWork.UserManager.FindByEmailAsync(user.Email!);
                if (result != null) {
                    if (result.EmailConfirmed == false)
                    {
                        //create  token to verify Email
                        string token =await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(result);
                         result.ConfirmToken = token; 
                        await unitOfWork.CommitAsync();

                        try
                        { await emailSender.SendEmailAsync(result.Email, "Verify Email", "Please verify your email by clicking on this link <a href='https://localhost:7251/api/Account/VerifyEmail?Id=" + result.Id.ToString() + "&Token=" + Uri.EscapeDataString(result.ConfirmToken) + "'>Verify EMAIL</a>");

                                }
                        catch(Exception ex)
                        {
                            return BadRequest("Failed to send email: " + ex.Message);
                        }



                        return Ok("Registration successful. Check email for confirmation.");


                    }
                    else
                    {
                        ModelState.AddModelError("", "The email already Confirmed.");
                     return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The email not Exist.");
                    return BadRequest(ModelState);

                }




            }
            return BadRequest(ModelState);




        }

        [HttpGet("VerifyEmail")]

        public async Task<IActionResult>  VerifyEmail(string Id,string token)
        {

            var user = unitOfWork.UserManager.Users.FirstOrDefault(x => x.Id.ToString() == Id);

            if(user == null || user.ConfirmToken !=token) return BadRequest("Invalid confirmation token");

            else
            {
                user.ConfirmToken = null;
                user.EmailConfirmed = true; 
                await unitOfWork.CommitAsync();
                return Ok("Email confirmed successfully");
            }



        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(loginRequest.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid login details");
                }
                var passwordValid = await unitOfWork.UserManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!passwordValid)
                {
                    return Unauthorized("Invalid login details");
                }
                if (!user.EmailConfirmed)
                {
                    return Unauthorized("Email not confirmed");
                }
                return Ok("Login successful");

            }
            return BadRequest("Invalid login request");
        }







        }
}
