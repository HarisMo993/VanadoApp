using MachineCheckup.Domain.Entities;
using System.Linq.Expressions;

namespace MachineCheckup.Application.Interfaces.Generics
{
    public interface IGenericService<T> where T : class
    {
        Task<PagedList<T>> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, int pageSize = 10, int pageNumber = 1);
        Task<T> GetById(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
