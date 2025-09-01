using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
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
        private IStudentRepository _studentRepository;
        private IClassRepository _classRepository;
        private ICourseRepository _courseRepository;
        private IStaffRepository _staffRepository;
        private ICourseClassRepository _courseClassRepository;
        private IEnrollmentRepository _enrollmentRepository;

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

        public IStudentRepository StudentRepository
        {
            get
            {
                if (this._studentRepository == null)
                {
                    this._studentRepository = new StudentRepository(_context);
                }
                return _studentRepository;
            }
        }

        public IClassRepository ClassRepository
        {
            get
            {
                if (this._classRepository == null)
                {
                    this._classRepository = new ClassRepository(_context);
                }
                return _classRepository;
            }
        }

        public ICourseRepository CourseRepository
        {
            get
            {
                if (this._courseRepository == null)
                {
                    this._courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
        }

        public IStaffRepository StaffRepository
        {
            get
            {
                if (this._staffRepository == null)
                {
                    this._staffRepository = new StaffRepository(_context);
                }
                return _staffRepository;
            }
        }

        public ICourseClassRepository CourseClassRepository
        {
            get
            {
                if (this._courseClassRepository == null)
                {
                    this._courseClassRepository = new CourseClassRepository(_context);
                }
                return _courseClassRepository;
            }
        }

        public IEnrollmentRepository EnrollmentRepository
        {
            get
            {
                if (this._enrollmentRepository == null)
                {
                    this._enrollmentRepository = new EnrollmentRepository(_context);
                }
                return _enrollmentRepository;
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
