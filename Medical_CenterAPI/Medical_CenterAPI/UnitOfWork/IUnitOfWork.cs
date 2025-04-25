using Medical_CenterAPI.Repository;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Medical_CenterAPI.UnitOfWork
{
    public interface IUnitOfWork
    {

         IRepository<Doctor> Doctors { get; }

         IRepository<Patient> Patients { get; }

        IRepository<Assistant> Assistants { get; }

        IRepository<Appointment> Appointments { get; }

        IRepository<AppointmentConfirmation> AppointmentsConfirmations { get; }
        UserManager<AppUser> UserManager { get;  } 
      RoleManager<IdentityRole<Guid>> RoleManager { get; } 
      SignInManager<AppUser> signInManager {  get; }

         IJWTTokenRepository JWTTokenRepository { get; }  
         Task<int> CommitAsync();
     
        
    }
}
