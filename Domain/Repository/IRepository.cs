using Domain.Entities;
using Domain.Entities.Contracts;

namespace Domain.Repository;
public interface IRepository<T>
{
    
    void Create(T entity);
    T GetById(Guid id);
    T GetById(int id, int id2);
    void Update(T entity);
    void Delete(T entity);
}