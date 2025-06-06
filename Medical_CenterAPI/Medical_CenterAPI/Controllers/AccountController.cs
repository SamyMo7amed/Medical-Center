﻿using AutoMapper;
using Castle.Core.Smtp;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Medical_CenterAPI.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Medical_CenterAPI.Service.IEmailSender emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, Medical_CenterAPI.Service.IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            this.emailSender = emailSender;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {

                var User = new Patient() { UserName=registerUser.UserName ,Email=registerUser.Email};

                var check = await unitOfWork.UserManager.FindByEmailAsync(registerUser.Email);
                if (check != null)
                {
                    ModelState.AddModelError("", "The email already exists.");
                    return BadRequest(ModelState);
                }
                else
                {

                 
                    

                    if (!await unitOfWork.RoleManager.RoleExistsAsync("Patient"))
                    {
                        await unitOfWork.RoleManager.CreateAsync(new IdentityRole<Guid>("Patient"));
                        await unitOfWork.CommitAsync();
                    }
                      var result = await unitOfWork.UserManager.CreateAsync(User, registerUser.Password);

                     var roleresult = await unitOfWork.UserManager.AddToRoleAsync(User,"Patient");


                    
                    if (result.Succeeded&&roleresult.Succeeded)
                    {
                        
                        await unitOfWork.CommitAsync();

                        return Ok(new { Message = "User registered successfully" });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }






            }

            return BadRequest(ModelState);

        }
        [HttpPost("AddEmployee")]

        [Authorize(Policy ="Manager")]
        public async Task<IActionResult> RegisterEmployee([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {

               
                var check = await unitOfWork.UserManager.FindByEmailAsync(employeeDTO.Email);
                if (check != null)
                {

                    ModelState.AddModelError("", "This Employee is Exist");
                    return BadRequest(ModelState);
                }
                else
                { 
                    
                    
                    
                    
                    
                    


                    AppUser emp;

                    IdentityResult result;
                    if (employeeDTO.Specialization == null)
                    {
                    var assistant = mapper.Map<Assistant>(employeeDTO);
                        
                      result=  await unitOfWork.UserManager.CreateAsync(assistant, employeeDTO.Password);
                        emp= await unitOfWork.Assistants.GetByIdAsync(assistant.Id);
                        await unitOfWork.UserManager.AddToRoleAsync(assistant, "Assistant");
                    }

                    else
                    {
                         var  doctor = mapper.Map<Doctor>(employeeDTO);
                        result= await unitOfWork.UserManager.CreateAsync(doctor, employeeDTO.Password);
                        emp=await  unitOfWork.Doctors.GetByIdAsync(doctor.Id);
                        await unitOfWork.UserManager.AddToRoleAsync(doctor, "Doctor");

                    }

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest(ModelState);
                    }
                
                 
                    
                   
                 
                    await unitOfWork.CommitAsync();
                    return Ok("Employee registered successfully");
                }



            }
            else
            {

                return BadRequest(ModelState);
            }
        }
        [HttpPost("ConfirmEmail")]

        public async Task<IActionResult> SaveRegister(ConfirmDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<AppUser>(UserFromRequest);
                var result = await unitOfWork.UserManager.FindByEmailAsync(user.Email!);
                if (result != null)
                {
                    if(result.Password!=UserFromRequest.Password) return BadRequest("Password Error");
                    if (result.EmailConfirmed == false)
                    {
                        //create  token to verify Email
                        string token = await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(result);
                        result.ConfirmToken = token;
                        await unitOfWork.CommitAsync();

                        try
                        {
                            await emailSender.SendEmailAsync(result.Email, "Verify Email", "Please verify your email by clicking on this link <a href='https://tantamedicalcenter12.runasp.net/api/Account/VerifyEmailToken?Id=" + result.Id.ToString() + "&Token=" + Uri.EscapeDataString(result.ConfirmToken) + "'>Verify EMAIL</a>");

                        }
                        catch (Exception ex)
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

        [HttpGet("VerifyEmailToken")]

        public async Task<IActionResult> VerifyEmail(string Id, string token)
        {

            var user = unitOfWork.UserManager.Users.FirstOrDefault(x => x.Id.ToString() == Id);

            if (user == null || user.ConfirmToken != token) return BadRequest("Invalid confirmation token");

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
        public async Task<IActionResult> Login([FromBody] Medical_CenterAPI.ModelDTO.LoginRequst loginRequest)
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

                    ModelState.AddModelError("", "The Email Or Password Is Wrong");
                    return BadRequest(ModelState);
                }
                if (!user.EmailConfirmed)
                {
                    return BadRequest("Email not confirmed");
                }
                string token = await unitOfWork.JWTTokenRepository.GetJWTTokenAsync(user);

                return Ok(token);

            }


            return BadRequest(ModelState);
        }


        [HttpDelete("DeleteEmployee")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {

            var patient = await unitOfWork.Patients.GetByIdAsync(id);


            var DepDoctors = await unitOfWork.Doctors.GetByIdAsync(id);
            var DepAssistants = await unitOfWork.Assistants.GetByIdAsync(id);
            if (DepDoctors == null && DepAssistants == null&&patient!=null)
            {

               

                await unitOfWork.Patients.DeleteAsync(patient.Id);   


            }
            else if (DepDoctors == null && DepAssistants != null)
            {
                
                await unitOfWork.Assistants.DeleteAsync(id);
             
            }
            else if (DepDoctors != null && DepAssistants == null)
            {
                
                await unitOfWork.Doctors.DeleteAsync(id);
                

            }
            await unitOfWork.CommitAsync();
            return Ok("Deleted Successfully");

        }

        [HttpPost("UpdateEmployee")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Update ([FromForm] UpdateDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {

                var emp= await unitOfWork.UserManager.FindByNameAsync(employeeDTO.UserName);
                if (emp == null)
                {
                    return BadRequest("The Employee not Exist");

                }
                else

                {
                    var DepDoctors = await unitOfWork.Doctors.GetByIdAsync(emp.Id);
                    
                    var DepAssistants = await unitOfWork.Assistants.GetByIdAsync(emp.Id);

                  
                   
                    if (DepAssistants== null)
                    {
                        
                        DepDoctors.PhoneNumber = employeeDTO.PhoneNumber;
                        DepDoctors.Specialization = employeeDTO.Specialization;

                      

                        

                       
                    
                    }
                    else
                    {
                        DepAssistants.PhoneNumber = employeeDTO.PhoneNumber;

                     

                       

                        




                    }
                    await unitOfWork.CommitAsync();


                    return Ok("Updated Successfully");



                }






            }


            else
            {
                return BadRequest(ModelState);
            }



        }


    }
}
