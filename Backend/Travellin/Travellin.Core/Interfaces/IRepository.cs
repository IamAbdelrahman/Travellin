namespace Travellin.Travellin.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
        Task SaveAsync();
    }
}
