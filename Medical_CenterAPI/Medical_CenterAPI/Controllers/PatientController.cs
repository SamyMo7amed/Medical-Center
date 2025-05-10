using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
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

        [HttpPost("UpdateMedicalHistoryJson/{patientId}")]
        [Authorize(Policy = "Patient")]
        public async Task<IActionResult> UpdateMedicalHistoryJson([FromRoute] Guid patientId, [FromBody] JsonElement json)
        {
            var patient = await _Service.GetByIdAsync(patientId);
            if (patient == null)
                return NotFound("Patient not found");

      
            patient.MedicalHistoryJson = json.GetRawText();

    
            var history = patient.MedicalHistory;
            history["UpdatedAt"] = DateTime.UtcNow.ToString("u");

          
            patient.MedicalHistory = history;

          await _Service.UpdateAsync(patient);
            await _Service.SaveChangesAsync();  

            return Ok(patient.MedicalHistory); 
        }










    }
}
