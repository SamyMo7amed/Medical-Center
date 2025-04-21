using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Repository
{
    public class AssistantRepository : IRepository<Assistant>
    {
        public Task AddAsync(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Assistant>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Assistant> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Assistant entity)
        {
            throw new NotImplementedException();
        }
    }
}
