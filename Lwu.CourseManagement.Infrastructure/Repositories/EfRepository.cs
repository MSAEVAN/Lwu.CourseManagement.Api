using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class EfRepository<T>(ApplicationDbContext db) : IRepository<T> where T : class
    {
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await db.Set<T>().FindAsync(new object?[] { id }, ct);

        public Task AddAsync(T entity, CancellationToken ct = default)
        {
            db.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public void Update(T entity) => db.Set<T>().Update(entity);
        public void Remove(T entity) => db.Set<T>().Remove(entity);
        public IQueryable<T> Query() => db.Set<T>().AsQueryable();
    }
}
