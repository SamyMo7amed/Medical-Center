using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;

namespace Medical_CenterAPI.Service
{
    public class DoctorService : IService<Doctor>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task AddAsync(Doctor entity)
        {
            await _unitOfWork.Doctors.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor != null)
            {
                 _unitOfWork.Doctors.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
        }


        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _unitOfWork.Doctors.GetAllAsync();
        }

        
        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Doctors.GetByIdAsync(id);
        }


        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Doctor entity)
        {
            await _unitOfWork.Doctors.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }


    }
}
