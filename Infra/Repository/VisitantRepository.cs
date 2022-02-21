using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Queries;
using Domain.Repository;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;
public class VisitantRepository : IRepository<Visitant>
{
    public VisitantRepository(DataContext context)
    {
        Context = context;
    }

    public DataContext Context { get; set; }

    public void Create(Visitant entity)
    {
        Context.Visitant.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Visitant entity)
    {
        Context.Visitant.Remove(entity);
        Context.SaveChanges();
    }

    public Visitant? GetById(Guid id)
    {
        return Context.Visitant.FirstOrDefault(Queries<Visitant>.GetById(id));
    }

    public Visitant? GetById(int id, int id2)
    {
        return Context.Visitant.Find(id, id2);
    }

    public void Update(Visitant entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
    }
}