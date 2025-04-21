using System.Runtime.CompilerServices;

namespace Medical_CenterAPI.Repository
{
    public interface IRepository< T> where T : class
    {
        Task<T> GetByIdAsync(Guid id ); 
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync( T entity );
        void UpdateAsync( T entity );   
        void DeleteAsync( Guid id );
        void SaveChangesAsync();

    }
    
}
