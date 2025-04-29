using AutoMapper;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentConfirmationController : ControllerBase
    {


        private readonly IService<AppointmentConfirmation> _service;
        private readonly IMapper mapper;

        public AppointmentConfirmationController(IService<AppointmentConfirmation> service, IMapper mapper)
        {
            this.mapper = mapper;
            _service = service;
        }

        [HttpGet("AppConfirmaitons")]
        public async Task<IActionResult> GetAppointmentConfirmationById(Guid id)
        {
            var app = await _service.GetByIdAsync(id);
            if (app == null)
            {
                return NotFound();
            }
            else return Ok(app);


        }

        [HttpGet("AllAppConfirmations")]


        public async Task<IActionResult> GetAll()
        {
            var appointments = await _service.GetAllAsync();
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);


        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AppointmentConfirmationDTO appointmentConfirmation1)
        {
            var app = mapper.Map<AppointmentConfirmation>(appointmentConfirmation1);

            var appointmentConfirmation = await _service.GetByIdAsync(app.Id);
            if (appointmentConfirmation == null)
            {

                await _service.AddAsync(app);
                return Ok("Successful");

            }

            return BadRequest("this already exist");


        }
        [HttpPost("Update")]

        public async Task<IActionResult> Update([FromBody] AppointmentConfirmationDTO appointmentConfirmation1)
        {
            var app = mapper.Map<AppointmentConfirmation>(appointmentConfirmation1);

            var appointmentConfirmation = await _service.GetByIdAsync(app.Id);
            if (appointmentConfirmation == null)
            {
                return NotFound("The appointmentConfirmation you want to update not exist");
            }
            else
            {

                await _service.UpdateAsync(app); return Ok("Successful");

            }


        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete(Guid id)
        {


            var app = await _service.GetByIdAsync(id);
            if (app == null)
            {
                     await _service.DeleteAsync(id);
            }
            

            return Ok("Delete Successfully");

        }

    }
}
