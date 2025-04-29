using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;

namespace Medical_CenterAPI.Service
{
    public class AppointmentService : IService<Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Appointment entity)
        {
            await _unitOfWork.Appointments.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async void DeleteAsync(Guid id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment != null)
            {
                 _unitOfWork.Appointments.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _unitOfWork.Appointments.GetAllAsync();
        }

        public async Task<Appointment> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Appointments.GetByIdAsync(id);
        }

        public async void SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async void UpdateAsync(Appointment entity)
        {
             _unitOfWork.Appointments.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
