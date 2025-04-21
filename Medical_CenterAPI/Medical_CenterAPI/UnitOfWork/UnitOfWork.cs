using Medical_CenterAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Medical_CenterAPI.UnitOfWork
{
    public class UnitOFWork : IUnitOfWork
    {
        public UnitOFWork(DoctorRepositrory doctor,PatientRepository patient,AssistantRepository assistant,AppointmentRepository appointment
            ,AppointmentConfirmationRepository appointmentConfirmation,UserManager<IdentityUser>
                userManager,RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager) 
        { 
            this.signInManager = signInManager;
            this.RoleManager = roleManager; 
            this.UserManager = userManager;
            this.Patients = patient;
            this.Assistants = assistant;
            this.Doctors = doctor;
            this.Appointments = appointment;    
            this.AppointmentsConfirmations = appointmentConfirmation; 

        }
        public DoctorRepositrory Doctors { get; }

        public PatientRepository Patients {get;}

        public AssistantRepository Assistants {get;}

        public AppointmentRepository Appointments {get;}

        public AppointmentConfirmationRepository AppointmentsConfirmations {get;}

        public UserManager<IdentityUser> UserManager {get;}

        public RoleManager<IdentityRole> RoleManager {get;}

        public SignInManager<IdentityUser> signInManager {get;}
    }
}
