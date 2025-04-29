using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;

namespace Medical_CenterAPI.Service
{
    public class AppointmentConfirmationService : IService<AppointmentConfirmation>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentConfirmationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AppointmentConfirmation entity)
        {
            await _unitOfWork.AppointmentsConfirmations.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var confirmation = await _unitOfWork.AppointmentsConfirmations.GetByIdAsync(id);
            if (confirmation != null)
            {
                 _unitOfWork.AppointmentsConfirmations.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<AppointmentConfirmation>> GetAllAsync()
        {
            return await _unitOfWork.AppointmentsConfirmations.GetAllAsync();
        }

        public async Task<AppointmentConfirmation?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.AppointmentsConfirmations.GetByIdAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(AppointmentConfirmation entity)
        {
           await  _unitOfWork.AppointmentsConfirmations.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
