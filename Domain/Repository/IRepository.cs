namespace Domain.Repository;
public interface IRepository<T>
{
    void Create(T entity);
    T? GetById(int Number, int Block, Guid id);
    T? GetById(int Number, int Block);
    T? GetById(Guid id);
    void Update(T entity);
    void Delete(T entity);
}