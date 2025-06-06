﻿using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;


using Medical_CenterAPI.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssistantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IService<Assistant> _Service;
        private readonly Medical_CenterAPI.Service.IEmailSender _EmailSender;
        private readonly IMapper mapper;


        public AssistantController(IService<Assistant> assistantService, Medical_CenterAPI.Service.IEmailSender emailSender,IUnitOfWork unitOfWork,IMapper mapper)
        {

            this.mapper = mapper;   
            _unitOfWork = unitOfWork;   
            _EmailSender = emailSender;
            _Service = assistantService;
        }

        [HttpGet("GetAssistant")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ass = await _Service.GetByIdAsync(id);
            if (ass == null)
            {

                return NotFound();
            }
            return Ok(ass);

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var ass = await _Service.GetAllAsync();
            if (ass == null)
            {

                return NotFound();
            }

            return Ok(ass);
        }



        [HttpGet("SendConfirmAppointment")]
        public async Task<IActionResult> SendConfirmAppointmentEmail(Guid id)
        {

            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment.DoctorId == null) return NotFound("the doctor in appointment");
           



                var patient = await _unitOfWork.Patients.GetByIdAsync(appointment.PatientId?? Guid.Empty);
            


            var user = mapper.Map<AppUser>(patient);
             
            string token = await _unitOfWork.UserManager.GenerateUserTokenAsync(user, "Email", "ConfirmAppointment");

            patient.Emailtoken = token; 
          
           

            try
            {
                await _EmailSender.SendEmailAsync(patient.Email!, "Confirm_Appointment", "Please confirm your appointment by  clicking on this link <a href='https://tantamedicalcenter12.runasp.net/api/Assistant/VerifyEmailToken?Id=" + user.Id.ToString() + "&Token=" + Uri.EscapeDataString(patient.Emailtoken) + "&Appointment="+appointment.AppointmentId+"'>Verify EMAIL</a>");

            }
            catch (Exception ex)
            {
                return BadRequest("Failed to send email: " + ex.Message);
            }



            return Ok("Please check your Email");
        }


        [HttpGet("VerifyEmailToken")]

        public async Task<IActionResult> VerifyEmail(string Id, string token,string Appointment)
        {

            var user = await _unitOfWork.UserManager.Users.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            Guid id = Guid.Parse(Appointment);
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            bool Isval = await _unitOfWork.UserManager.VerifyUserTokenAsync(user, "Email", "ConfirmAppointment", token);
             var patient= mapper.Map<Patient>(user); 



            if (user == null || !Isval|| patient.Emailtoken!=token) return BadRequest("Invalid confirmation token");

            else
            {

               patient.Emailtoken = null;
                appointment.Status = Appointment_Status.Confirmed;
                await _unitOfWork.CommitAsync();
                return Ok("Email confirmed successfully");
            }



        }





    }
}
