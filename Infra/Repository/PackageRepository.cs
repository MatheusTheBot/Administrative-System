using Domain.Entities;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class PackageRepository : IRepository<Packages>
{
    public PackageRepository(DataContext context)
    {
        Context = context;
    }

    public DataContext Context { get; set; }

    public void Create(Packages entity)
    {
        Context.Packages.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Packages entity)
    {
        Context.Packages.Remove(entity);
        Context.SaveChanges();
    }

    public Packages GetById(Guid id)
    {
        return Context.Packages.FirstOrDefault(Queries<Packages>.GetById(id));
    }

    public Packages GetById(int id, int id2)
    {
        return Context.Packages.Find(id, id2);
    }

    public void Update(Packages entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
    }
}