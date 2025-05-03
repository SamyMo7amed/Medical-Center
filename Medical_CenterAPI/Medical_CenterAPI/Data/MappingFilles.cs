using AutoMapper;
using Medical_CenterAPI.ModelDTO;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Medical_CenterAPI.Data
{
    public class MappingFilles : Profile
    {

        public MappingFilles() {
            CreateMap<Patient, RegisterUser>();
            CreateMap<RegisterUser,Patient>();

            CreateMap<Appointment,AppointmentDTO>();  
            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<AppointmentConfirmationDTO, AppointmentConfirmation>();   
            CreateMap<AppointmentConfirmation, AppointmentConfirmationDTO>();
            CreateMap<EmployeeDTO, Doctor>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<EmployeeDTO,Assistant>().ForMember(dest => dest.Id, opt => opt.Ignore()); ; 
            CreateMap<Assistant,AppUser>();
            CreateMap<Doctor,AppUser>();
            CreateMap<AppUser,Patient>();   
            CreateMap<Patient,AppUser>();   
            CreateMap<EmployeeDTO,Doctor>();
            CreateMap<EmployeeDTO,Assistant>();
            CreateMap<Appointment, UpdateAppointment>();
            CreateMap<ConfirmDTO,AppUser>();    
            CreateMap<UpdateAppointment, Appointment>();
        }  
    }
}
