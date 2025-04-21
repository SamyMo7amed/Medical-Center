using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        Task IRepository<Appointment>.AddAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Appointment>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Appointment>> IRepository<Appointment>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Appointment> IRepository<Appointment>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Appointment>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IRepository<Appointment>.UpdateAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
 