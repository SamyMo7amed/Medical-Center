using AutoMapper;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly  IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AccountController(IUnitOfWork unitOfWork,IMapper mapper) {
         this.unitOfWork = unitOfWork;                  
        this.mapper = mapper;   
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUser registerUser)
         {
            if (ModelState.IsValid) {

                var User = mapper.Map<Patient>(registerUser);   
                var check= await unitOfWork.UserManager.FindByEmailAsync(registerUser.Email); 
                if(check != null)
                {
                    ModelState.AddModelError("", "The email already exists.");
                    return Ok(new {message= "The email already exists." } );
                }
                else {  var result= await unitOfWork.UserManager.CreateAsync(User,User.Password);
                    if (result.Succeeded)
                    {

                        return Ok(new { Message = "User registered successfully" });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
            
             
            
           
            
            
            }

            return  BadRequest(ModelState);

        }



       




    }
}
