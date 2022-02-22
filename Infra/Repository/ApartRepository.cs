using Domain.Entities;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class ApartRepository : IRepository<Apart>
{
    public DataContext Context { get; set; }
    public ApartRepository(DataContext context)
    {
        Context = context;
    }

    public void Create(Apart entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc == null)
        {
            Context.Aparts.Add(entity);
            Context.SaveChanges();
        }
    }

    public void Delete(Apart entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Aparts.Remove(entity);
            Context.SaveChanges();
        }
    }

    public Apart? GetById(Guid id)
    {
        return null;
    }

    public Apart? GetById(int id, int id2)
    {
        var result = Context.Aparts.Find(id, id2);
        if (result != null)
        {
            Context.Entry(result).Collection(x => x.Packages).Load();
            Context.Entry(result).Collection(x => x.Visitants).Load();
            Context.Entry(result).Collection(x => x.Residents).Load();
            return result;
        }
        else
            return null;
    }
    public void Update(Apart entity)
    {
        var sc = Context.Find<Apart>(entity.Number, entity.Block);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Aparts.Update(entity);
            Context.SaveChanges();
        }
    }
}