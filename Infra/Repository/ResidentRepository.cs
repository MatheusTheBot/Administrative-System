using Domain.Entities;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class ResidentRepository : IRepository<Resident>
{
    public DataContext Context { get; set; }
    public ResidentRepository(DataContext context)
    {
        Context = context;
    }

    public void Create(Resident entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Residents.Add(entity);
            Context.SaveChanges();
        }
    }

    public void Delete(Resident entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Residents.Remove(entity);
            Context.SaveChanges();
        }
    }

    public Resident? GetById(Guid id)
    {
        return Context.Residents.FirstOrDefault(Queries<Resident>.GetById(id));
    }

    public Resident? GetById(int id, int id2)
    {
        return null;
    }

    public Resident? GetById(int Number, int Block, Guid id)
    {
        return Context.Residents.Where(x => x.Number == Number && x.Block == Block).FirstOrDefault(Queries<Resident>.GetById(id));
    }

    public void Update(Resident entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Residents.Update(entity);
            Context.SaveChanges();
        }
    }
}