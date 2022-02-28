using Domain.Entities;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class VisitantRepository : IRepository<Visitant>
{
    public DataContext Context { get; set; }
    public VisitantRepository(DataContext context)
    {
        Context = context;
    }

    public void Create(Visitant entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Visitant.Add(entity);
            Context.SaveChanges();
        }
    }

    public void Delete(Visitant entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Visitant.Remove(entity);
            Context.SaveChanges();
        }
    }

    public Visitant? GetById(Guid id)
    {
        return Context.Visitant.FirstOrDefault(Queries<Visitant>.GetById(id));
    }

    public Visitant? GetById(int id, int id2)
    {
        return null;
    }

    public Visitant? GetById(int Number, int Block, Guid id)
    {
        return Context.Visitant.Where(x=> x.Number == Number && x.Block == Block).FirstOrDefault(Queries<Visitant>.GetById(id));
    }

    public void Update(Visitant entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Visitant.Update(entity);
            Context.SaveChanges();
        }
    }
}