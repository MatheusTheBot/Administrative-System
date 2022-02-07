namespace Domain.Repository;
public interface IRepository<T>
{
    void Create(T entity);
    T GetById(Guid id);
    void Update(T entity);
    void Delete(T entity);
}