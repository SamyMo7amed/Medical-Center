using Medical_CenterAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Medical_CenterAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DoctorRepositrory doctor,PatiantRepository patiant,AssistantRepository assistant,AppointmentRepository appointment
            ,AppointmentConfirmationRepository appointmentConfirmation,UserManager<IdentityUser>
                userManager,RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager) 
        { 
            this.signInManager = signInManager;
            this.RoleManager = roleManager; 
            this.UserManager = userManager;
            this.Patiants = patiant;
            this.Assistants = assistant;
            this.Doctors = doctor;
            this.Appointments = appointment;    
            this.AppointmentsConfirmations = appointmentConfirmation; 

        }
        public DoctorRepositrory Doctors { get; }

        public PatiantRepository Patiants {get;}

        public AssistantRepository Assistants {get;}

        public AppointmentRepository Appointments {get;}

        public AppointmentConfirmationRepository AppointmentsConfirmations {get;}

        public UserManager<IdentityUser> UserManager {get;}

        public RoleManager<IdentityRole> RoleManager {get;}

        public SignInManager<IdentityUser> signInManager {get;}
    }
}
