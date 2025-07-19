namespace Travellin.Travellin.Core.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetByID(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }
}
