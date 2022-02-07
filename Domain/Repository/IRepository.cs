namespace Domain.Repository;
public interface IRepository<T>
{
    void Create(T entity);
    void Read(T entity);
    void Update(T entity);
    void Delete(T entity);
}