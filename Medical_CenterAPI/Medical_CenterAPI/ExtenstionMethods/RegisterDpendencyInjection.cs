using Medical_CenterAPI.Models;
using Medical_CenterAPI.Repository;
using Medical_CenterAPI.Service;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace Medical_CenterAPI.ExtenstionMethods
{
    public static class RegisterDpendencyInjection
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Appointment>,AppointmentRepository>();
            services.AddScoped<IRepository<Doctor>,DoctorRepositrory>();
            services.AddScoped<IRepository<AppointmentConfirmation>,AppointmentConfirmationRepository>();
            services.AddScoped<IRepository<Assistant>,AssistantRepository>();
            services.AddScoped<IRepository<Patient>,PatientRepository>();
            services.AddScoped<IService<Appointment>,AppointmentService>();
            services.AddScoped<IService<AppointmentConfirmation>,AppointmentConfirmationService>();
            services.AddScoped<IService<Doctor>,DoctorService>();
            services.AddScoped<IService<Assistant>,AssistantService>();
            services.AddScoped<IService<Patient>,PatientService>();
            services.AddScoped<IUnitOfWork,UnitOFWork>();

        }
    }
}
