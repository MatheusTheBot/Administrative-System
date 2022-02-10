using Domain.Entities;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class ApartRepository : IRepository<Apart>
{
    public ApartRepository(DataContext context)
    {
        Context = context;
    }
    public DataContext Context { get; set; }

    public void Create(Apart entity)
    {
        Context.Aparts.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Apart entity)
    {
        Context.Aparts.Remove(entity);
        Context.SaveChanges();
    }

    public Apart GetById(Guid id)
    {
        return null; //
    }

    public Apart GetById(int id, int id2)
    {
        return Context.Aparts.Find(id, id2);
    }

    public void Update(Apart entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.Aparts.Update(entity);
        Context.SaveChanges();
    }
}