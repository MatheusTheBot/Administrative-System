using Domain.Entities;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class ResidentRepository : IRepository<Resident>
{
    public ResidentRepository(DataContext context)
    {
        Context = context;
    }

    public DataContext Context { get; set; }

    public void Create(Resident entity)
    {
        Context.Residents.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Resident entity)
    {
        Context.Residents.Remove(entity);
        Context.SaveChanges();
    }

    public Resident? GetById(Guid id)
    {
        return Context.Residents.FirstOrDefault(Queries<Resident>.GetById(id));
    }

    public Resident? GetById(int id, int id2)
    {
        return Context.Residents.Find(id, id2);
    }

    public void Update(Resident entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.Residents.Update(entity);
        Context.SaveChanges();
    }
}