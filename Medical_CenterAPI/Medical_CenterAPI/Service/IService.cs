namespace Medical_CenterAPI.Service
{
    public interface IService<T> where T : class 
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);    
        Task UpdateAsync(T entity); 
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();   

    }
    
}
