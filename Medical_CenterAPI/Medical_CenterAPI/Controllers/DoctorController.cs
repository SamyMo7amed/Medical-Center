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
    public class DoctorController : ControllerBase
    {
        private readonly IService<Doctor> _Service;

       public DoctorController(IService<Doctor> service)
        {
            _Service = service; 
        }
        [HttpGet("GetDoctor")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dos = await _Service.GetByIdAsync(id);
            if (dos == null)
            {

                return NotFound();
            }
            return Ok(dos);

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var dos = await _Service.GetAllAsync();
            if (dos == null)
            {

                return NotFound();
            }

            return Ok(dos);
        }


    }
}
