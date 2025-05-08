using AutoMapper;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AppointmentController : ControllerBase
    {
        private readonly IService<Appointment> _service;
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentController(IService<Appointment> service, IMapper mapper,IUnitOfWork unitOfWork)
        {this.mapper = mapper;  
            this._unitOfWork = unitOfWork;  
           
            _service = service;
        }


        [HttpGet("GetAppointment")]
        public async Task<IActionResult> GetById(Guid id)
        {


            var appointment = await _service.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(appointment);
            }


        }

        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _service.GetAllAsync();
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);


        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]AppointmentDTO appointment1)

        {
            var appointment = mapper.Map<Appointment>(appointment1);
            var patient= await _unitOfWork.Patients.GetByIdAsync(appointment1.PatientId);
            var doctor= await _unitOfWork.Doctors.GetByIdAsync(appointment1.DoctorId);
           
             var app= await _service.GetByIdAsync(appointment.AppointmentId);
         


            if (appointment == null)
            {



                return NotFound("NotFound this appointment");
            }

            bool flag = false;
            foreach (var appi in doctor.Appointments)
            {
                if (appointment.AppointmentDate.Date == appi.AppointmentDate.Date&& appointment.AppointmentDate.Hour==appi.AppointmentDate.Hour) 
                flag = true;
                
            }


            if (flag) return BadRequest("This appointment is not available");
           
                if (app == null && patient!=null&& doctor!=null )
            {



                appointment.Status = Appointment_Status.Pending;
                await _service.AddAsync(appointment);


                await _service.SaveChangesAsync();

                return Ok("Added Successfully");
            

            }
            return BadRequest("please check the Form data");


        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete( Guid id)
        {

            var app=await _service.GetByIdAsync(id);
            if(app == null)
            {
      

             return BadRequest("this already not in database");
            }



            await _service.DeleteAsync(id);

            await _service.SaveChangesAsync();

            return Ok("Deleted Successfully");





        }

        [HttpPost("Update")]

        public async Task<IActionResult> Update([FromBody] UpdateAppointment updateAppointment)
        {

            var app=await _unitOfWork.Appointments.GetByIdAsync(updateAppointment.AppointmentId);
            if (app == null)
            {
                return BadRequest("the appointment not exist");
            }
            var appointment = mapper.Map<Appointment>(updateAppointment);
            await _service.UpdateAsync(appointment);
            return Ok("Successful");


        }


    }
}
