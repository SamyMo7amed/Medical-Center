using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Service
{
    public class PatientService : IService<Patient>
    {
        public Task AddAsync(Patient entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
