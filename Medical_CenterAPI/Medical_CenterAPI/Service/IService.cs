namespace Medical_CenterAPI.Service
{
    public interface IService<T> where T : class 
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);    
        void UpdateAsync(T entity); 
        void DeleteAsync(Guid id);
        void SaveChangesAsync();   

    }
    
}
