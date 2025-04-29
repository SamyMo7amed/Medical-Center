using Medical_CenterAPI.Models;
using Medical_CenterAPI.UnitOfWork;

namespace Medical_CenterAPI.Service
{
    public class AssistantService : IService<Assistant>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssistantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Assistant entity)
        {
            await _unitOfWork.Assistants.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var assistant = await _unitOfWork.Assistants.GetByIdAsync(id);
            if (assistant != null)
            {
                 _unitOfWork.Assistants.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Assistant>> GetAllAsync()
        {
            return await _unitOfWork.Assistants.GetAllAsync();
        }

        public async Task<Assistant?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Assistants.GetByIdAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Assistant entity)
        {
          await  _unitOfWork.Assistants.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
