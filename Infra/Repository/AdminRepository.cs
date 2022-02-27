using Domain.Entities;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class AdminRepository : IRepository<Administrator>
{
    public DataContext Context { get; set; }
    public AdminRepository(DataContext context)
    {
        Context = context;
    }
    public void Create(Administrator entity)
    {
        var sc = GetById(entity.Id);
        if (sc == null)
        {
            Context.Administrators.Add(entity);
            Context.SaveChanges();
        }
    }

    public void Delete(Administrator entity)
    {
        var sc = GetById(entity.Id);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Administrators.Remove(entity);
            Context.SaveChanges();
        }
    }

    public Administrator? GetById(Guid id)
    {
        return Context.Administrators.FirstOrDefault(Queries<Administrator>.GetById(id));
    }

    public Administrator? GetById(int id, int id2)
    {
        return null;
    }
    public void Update(Administrator entity)
    {
        var sc = GetById(entity.Id);
        if (sc != null)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Administrators.Update(entity);
            Context.SaveChanges();
        }
    }
}