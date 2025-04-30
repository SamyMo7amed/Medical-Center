using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IService<Patient> _Service;

        public PatientController(IService<Patient> service)
        
            {
                _Service = service; 
            }

        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pas = await _Service.GetByIdAsync(id);
            if (pas == null)
            {

                return NotFound();
            }
            return Ok(pas);

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var pas = await _Service.GetAllAsync();
            if (pas == null)
            {

                return NotFound();
            }

            return Ok(pas);
        }










    }
}
