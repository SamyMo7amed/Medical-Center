using AutoMapper;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IService<Appointment> _service;
        private readonly IMapper mapper;


        public AppointmentController(IService<Appointment> service,IMapper mapper)
        {this.mapper = mapper;  
            _service = service;
        }


        [HttpGet("GetAppointment")]
        public async Task<IActionResult> GetById([FromBody]Guid id)
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
             var app= await _service.GetByIdAsync(appointment.AppointmentId);
            if (app == null)
            {

            await _service.AddAsync(appointment);   

            return Ok("Successful");
            

            }
            return BadRequest("this already exist");


        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete([FromBody] Guid id)
        {

            var app=await _service.GetByIdAsync(id);
            if(app == null)
            {
            await _service.DeleteAsync(id);
            }

              
            return Ok("Delete Successfully");


        }

        [HttpPost("Update")]

        public async Task<IActionResult> Update([FromBody] AppointmentDTO appointment1)
        {
            var appointment = mapper.Map<Appointment>(appointment1);
            await _service.UpdateAsync(appointment);
            return Ok("Successful");


        }


    }
}
