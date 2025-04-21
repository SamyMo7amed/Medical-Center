using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Repository
{
    public class DoctorRepositrory : IRepository<Doctor>
    {
        Task IRepository<Doctor>.AddAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Doctor>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Doctor>> IRepository<Doctor>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Doctor> IRepository<Doctor>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Doctor>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IRepository<Doctor>.UpdateAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
