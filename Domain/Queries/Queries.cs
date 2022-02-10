using Domain.Entities;
using Domain.Entities.Contracts;
using System.Linq.Expressions;

namespace Domain.Queries;
public static class Queries<T> where T : Entity
{
    public static Expression<Func<T, bool>> GetById(Guid Id)
    {
        return x => x.Id == Id;
    }
}