namespace EFCore.Data.Repositories;

public interface IRepository<T> where T: class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(int id);
    Task<T?> Create(T value);
    Task<T?> Update(int id, T value);
    Task<bool> Delete(int id);

    IQueryable<T> All();
}