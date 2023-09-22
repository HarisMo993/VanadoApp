using MachineCheckup.Application.Interfaces.Generics;
using MachineCheckup.Domain.Entities;
using MachineCheckup.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MachineCheckup.Infrastructure.Data.Services.Generics
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task<PagedList<T>> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, int pageSize = 10, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Calculate the number of items to skip
            int skipAmount = pageSize * (pageNumber - 1);

            // Apply paging
            query = query.Skip(skipAmount).Take(pageSize);

            var pagedList = new PagedList<T>
            {
                Items = await query.AsNoTracking().ToListAsync(),
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = await _dbSet.CountAsync()
            };

            return pagedList;
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
