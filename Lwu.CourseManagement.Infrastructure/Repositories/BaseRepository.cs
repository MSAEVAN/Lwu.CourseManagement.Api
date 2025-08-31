using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }
        public T Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            return entity;
        }
        public void Delete(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Deleted;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate = null,
                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                  bool isDisableTracking = true)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            IQueryable<T> query = _dbContext.Set<T>();

            if (isDisableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }



        public ApplicationDbContext GetDbContext()
        {
            return _dbContext;
        }

        public U ExecuteScalar<U>(string query)
        {
            U retulst = default(U);
            using (var command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = System.Data.CommandType.Text;

                //command.Parameters.Add(parameter);

                this._dbContext.Database.OpenConnection();
                var tempResult = command.ExecuteScalar();
                if (tempResult != null && !tempResult.Equals(DBNull.Value))
                {
                    retulst = (U)tempResult;

                }


            }
            return retulst;
        }
        public List<U> ExecuteReader<U>(string query)
        {
            List<U> retulst = new List<U>();
            using (var command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = System.Data.CommandType.Text;

                //command.Parameters.Add(parameter);

                this._dbContext.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retulst.Add(reader.GetFieldValue<U>(0));
                    }
                }

            }
            return retulst;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.ChangeTracker.Clear();
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            return entities;
        }
    }
}
