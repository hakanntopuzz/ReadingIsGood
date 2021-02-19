using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }

        void Add(T entity);

        void Update(T entity);

        Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task<int> UpdateRangeAsync(IEnumerable<T> entity);

        Task<int> DeleteAsync(T entity);

        Task<int> DeleteRangeAsync(IEnumerable<T> entities);

        Task<int> CreateRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

        IQueryable<T> All();

        IQueryable<T> Where(Expression<Func<T, bool>> where);

        Task<List<T>> ToListAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}