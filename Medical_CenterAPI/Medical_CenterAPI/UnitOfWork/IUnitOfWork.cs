using Medical_CenterAPI.Repository;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Medical_CenterAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        
       DoctorRepositrory Doctors { get; }
       PatiantRepository Patiants { get; }
       AssistantRepository Assistants { get; } 
       AppointmentRepository Appointments { get; }
       AppointmentConfirmationRepository AppointmentsConfirmations { get; } 
      UserManager<IdentityUser> UserManager { get;  } 
      RoleManager<IdentityRole> RoleManager { get; } 
      SignInManager<IdentityUser> signInManager {  get; }    
     
        
    }
}
