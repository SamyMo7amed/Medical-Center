using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;

namespace Medical_CenterAPI.Service
{
    public class PatientService : IService<Patient>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Patient entity)
        {
            await _unitOfWork.Patients.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async void DeleteAsync(Guid id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient != null)
            {
                 _unitOfWork.Patients.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
        }


        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _unitOfWork.Patients.GetAllAsync();
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Patients.GetByIdAsync(id);
        }

        public async void SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async void UpdateAsync(Patient entity)
        {
             _unitOfWork.Patients.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

    }
}
