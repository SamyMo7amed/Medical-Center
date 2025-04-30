using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Register([FromForm] RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {

                var User = mapper.Map<Patient>(registerUser);
                var check = await unitOfWork.UserManager.FindByEmailAsync(registerUser.Email);
                if (check != null)
                {
                    ModelState.AddModelError("", "The email already exists.");
                    return BadRequest(ModelState);
                }
                else
                {

                    // to add image 

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    uniqueFileName += Path.GetExtension(registerUser.image.FileName);

                    string imageFullPath = webHostEnvironment.WebRootPath + "/Images/" + uniqueFileName;

                    User.ImagePath = imageFullPath;

                    var result = await unitOfWork.UserManager.CreateAsync(User, User.Password);
                    if (result.Succeeded)
                    {
                        using (var stream = System.IO.File.Create(imageFullPath))
                        {
                            registerUser.image.CopyTo(stream);
                        }

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

        public async Task<IActionResult> RegisterEmployee([FromForm] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                AppUser emp;

                if (employeeDTO.Specialization == null)
                {
                    var emp2 = mapper.Map<Assistant>(employeeDTO);
                    emp = mapper.Map<AppUser>(emp2);
                }
                else
                {
                    var emp2 = mapper.Map<Doctor>(employeeDTO);
                    emp = mapper.Map<AppUser>(emp2);
                }
                var check = await unitOfWork.UserManager.FindByEmailAsync(employeeDTO.Email);
                if (check != null)
                {

                    ModelState.AddModelError("", "This Employee is Exist");
                    return BadRequest(ModelState);
                }
                else
                {
                    // add image
                    var uniqueFilleName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString();
                    uniqueFilleName += Path.GetFileName(employeeDTO.image.FileName);

                    string ImageFullPath = webHostEnvironment.WebRootPath + "/Images/" + uniqueFilleName;
                    var result = await unitOfWork.UserManager.CreateAsync(emp, employeeDTO.Password);
                    using (var stream = System.IO.File.Create(ImageFullPath))
                    {
                        employeeDTO.image.CopyTo(stream);
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
        public async Task<IActionResult> SaveRegister(RegisterUser UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<Patient>(UserFromRequest);
                var result = await unitOfWork.UserManager.FindByEmailAsync(user.Email!);
                if (result != null)
                {
                    if (result.EmailConfirmed == false)
                    {
                        //create  token to verify Email
                        string token = await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(result);
                        result.ConfirmToken = token;
                        await unitOfWork.CommitAsync();

                        try
                        {
                            await emailSender.SendEmailAsync(result.Email, "Verify Email", "Please verify your email by clicking on this link <a href='https://localhost:7251/api/Account/VerifyEmailToken?Id=" + result.Id.ToString() + "&Token=" + Uri.EscapeDataString(result.ConfirmToken) + "'>Verify EMAIL</a>");

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
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {

            var DepDoctors = await unitOfWork.Doctors.GetByIdAsync(id);
            var DepAssistants = await unitOfWork.Assistants.GetByIdAsync(id);
            if (DepDoctors == null && DepAssistants == null)
            {
                return Ok();
            }
            else if (DepDoctors == null && DepAssistants != null)
            {
                await unitOfWork.Assistants.DeleteAsync(id);
                await unitOfWork.Assistants.SaveChangesAsync();
            }
            else if (DepDoctors != null && DepAssistants == null)
            {
                await unitOfWork.Doctors.DeleteAsync(id);
                await unitOfWork.Doctors.SaveChangesAsync();

            }

            return Ok();

        }

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> Update([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {

                var emp= await unitOfWork.UserManager.FindByEmailAsync(employeeDTO.Email);
                if (emp == null)
                {
                    return BadRequest("The Employee not Exist");

                }
                else

                {
                    var DepDoctors = await unitOfWork.Doctors.GetByIdAsync(emp.Id);
                    var DepAssistants = await unitOfWork.Assistants.GetByIdAsync(emp.Id);
                    if(DepAssistants== null)
                    { 
                        await unitOfWork.Doctors.UpdateAsync(DepDoctors);
                        await unitOfWork.CommitAsync();
                    
                    }
                    else
                    {
                        await unitOfWork.Assistants.UpdateAsync(DepAssistants);     

                        await unitOfWork.CommitAsync();
                    }

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
