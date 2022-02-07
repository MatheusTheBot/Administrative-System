using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Queries;
public static class ResidentQueries
{
    public static Expression<Func<Resident, bool>> GetById(Guid Id)
    {
        return x => x.Id == Id;
    }
}