using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_CenterAPI.Repository
{
    public class AppointmentConfirmationRepository : IRepository<AppointmentConfirmation>
    {
         private readonly AppDbContext _context;    
        public AppointmentConfirmationRepository(AppDbContext appDbContext) { 
          this._context = appDbContext;         
        }  
        public async Task AddAsync(AppointmentConfirmation entity)
        {
            await _context.AppointmentConfirmations.AddAsync(entity);
        }

        public void DeleteAsync(Guid id)
        {
            var appointmentConfirmation= _context.AppointmentConfirmations.FirstOrDefault(x => x.Id == id);
            if (appointmentConfirmation != null) { 
            _context.AppointmentConfirmations.Remove(appointmentConfirmation);
            }
        }

        public async Task<IEnumerable<AppointmentConfirmation>> GetAllAsync()
        {
            var result = await _context.AppointmentConfirmations.ToListAsync();

            return result;


        }

        public async Task<AppointmentConfirmation?> GetByIdAsync(Guid id)
        {
            var item= await _context.AppointmentConfirmations.FirstOrDefaultAsync(x => x.Id == id);
            return item;
            
        }

        public void SaveChangesAsync()
        {
          _context.SaveChanges();
        }

        public async Task UpdateAsync(AppointmentConfirmation entity)
        {
            var updatedAppointmentConfirmation = await _context.AppointmentConfirmations.FirstOrDefaultAsync(x => x.Id == entity.Id);
             updatedAppointmentConfirmation.Appointment=entity.Appointment;
            updatedAppointmentConfirmation.AppointmentId=entity.AppointmentId;
            updatedAppointmentConfirmation.AssistantId=entity.AssistantId;
            updatedAppointmentConfirmation.Assistant=entity.Assistant;
            updatedAppointmentConfirmation.ConfirmationDate=entity.ConfirmationDate;
            updatedAppointmentConfirmation.CreatedAt=entity.CreatedAt;
        }
    }
}
