using Medical_CenterAPI.Repository;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Medical_CenterAPI.UnitOfWork
{
    public interface IUnitOfWork
    {

        public IRepository<Doctor> Doctors { get; }

        public IRepository<Patient> Patients { get; }

        public IRepository<Assistant> Assistants { get; }

        public IRepository<Appointment> Appointments { get; }

        public IRepository<AppointmentConfirmation> AppointmentsConfirmations { get; }
        UserManager<IdentityUser> UserManager { get;  } 
      RoleManager<IdentityRole> RoleManager { get; } 
      SignInManager<IdentityUser> signInManager {  get; }    
     
        
    }
}
