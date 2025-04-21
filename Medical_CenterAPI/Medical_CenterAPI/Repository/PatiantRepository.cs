using Medical_CenterAPI.Models;

namespace Medical_CenterAPI.Repository
{
    public class PatiantRepository : IRepository<Patiant>
    {
        Task IRepository<Patiant>.AddAsync(Patiant entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Patiant>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Patiant>> IRepository<Patiant>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Patiant> IRepository<Patiant>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Patiant>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IRepository<Patiant>.UpdateAsync(Patiant entity)
        {
            throw new NotImplementedException();
        }
    }
}
