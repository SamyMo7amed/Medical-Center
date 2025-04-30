using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_CenterAPI.Repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {

        private readonly AppDbContext _context; 
        public AppointmentRepository(AppDbContext appDbContext) { 
        this._context = appDbContext;   
        }  
        public async Task AddAsync(Appointment entity)
        {
            await this._context.Appointments.AddAsync(entity);                       
        }

        public async Task DeleteAsync(Guid id)
        {

            var appointment =await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);
            if (appointment != null) { 
            
            _context.Appointments.Remove(appointment);                      
            }
            
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            var appointments= await _context.Appointments.ToListAsync();    

            return appointments;
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x=>x.AppointmentId==id );

            return appointment;
        }

        public async Task SaveChangesAsync()
        {
          await  _context.SaveChangesAsync();        }

        public async Task UpdateAsync(Appointment entity)
        {
             var appointment =  await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == entity.AppointmentId);

            appointment.Assistant = entity.Assistant;   
            appointment.AssistantId = entity.AssistantId;
            appointment.AppointmentConfirmationId = entity.AppointmentConfirmationId;                        
            appointment.AppointmentConfirmation=entity.AppointmentConfirmation; 
            appointment.AppointmentDate = entity.AppointmentDate;       
            appointment.CreatedAt = entity.CreatedAt;               
            appointment.Doctor = entity.Doctor; 
            appointment.DoctorId = entity.DoctorId; 
            appointment.Patiant=entity.Patiant;
            appointment.PatiantId = entity.PatiantId;
        }
    }
}
 