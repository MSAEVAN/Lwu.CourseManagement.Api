using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
using Lwu.CourseManagement.Application.Entities;
using Lwu.CourseManagement.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        #region Common Functions
        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DetachEntity<T>(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
