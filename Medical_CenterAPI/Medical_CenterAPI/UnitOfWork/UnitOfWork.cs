using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Medical_CenterAPI.UnitOfWork
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOFWork(IRepository<Doctor> doctor,IRepository<Patient> patient,IRepository<Assistant> assistant,IRepository<Appointment> appointment
            ,IRepository<AppointmentConfirmation> appointmentConfirmation,UserManager<AppUser>
                userManager,RoleManager<IdentityRole<Guid>> roleManager,SignInManager<AppUser> signInManager,AppDbContext appDbContext,IJWTTokenRepository jWTTokenRepository) 
        { 
            this.signInManager = signInManager;
            this.RoleManager = roleManager; 
            this.UserManager = userManager;
            this.Patients = patient;
            this.Assistants = assistant;
            this.Doctors = doctor;
            this.Appointments = appointment;    
            this.AppointmentsConfirmations = appointmentConfirmation; 
            this._appDbContext = appDbContext;  
            this.JWTTokenRepository = jWTTokenRepository;

        }
        public IRepository<Doctor>  Doctors { get; }

        public IRepository<Patient> Patients {get;}

        public IRepository<Assistant> Assistants {get;}

        public IRepository<Appointment> Appointments {get;}

        public IRepository<AppointmentConfirmation> AppointmentsConfirmations {get;}

        public UserManager<AppUser> UserManager {get;}

        public RoleManager<IdentityRole<Guid>> RoleManager {get;}

        public SignInManager<AppUser> signInManager {get;}

        public IJWTTokenRepository JWTTokenRepository { get;}

        public async Task<int>  CommitAsync()
        {

            return  await _appDbContext.SaveChangesAsync();
        }
        
    }
}
