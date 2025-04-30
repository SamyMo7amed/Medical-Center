using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistantController : ControllerBase
    {
        private readonly IService<Assistant> _Service;


        public AssistantController(IService<Assistant> assistantService)
        {
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





    }
}
