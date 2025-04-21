using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Repository
{
    public class AssistantRepository : IRepository<Assistant>
    {
        Task IRepository<Assistant>.AddAsync(Assistant entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Assistant>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Assistant>> IRepository<Assistant>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Assistant> IRepository<Assistant>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Assistant>.SaveChangesAsync()      
        {
            throw new NotImplementedException();
        }

        void IRepository<Assistant>.UpdateAsync(Assistant entity)
        {
            throw new NotImplementedException();
        }
    }
}
