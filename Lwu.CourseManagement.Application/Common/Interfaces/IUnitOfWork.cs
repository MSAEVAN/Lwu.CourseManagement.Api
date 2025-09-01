using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
using Lwu.CourseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IClassRepository ClassRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public IStaffRepository StaffRepository { get; }
        public ICourseClassRepository CourseClassRepository { get; }
        public IEnrollmentRepository EnrollmentRepository { get; }

        public int Commit();
        public Task<int> CommitAsync();
        public void DetachEntity<T>(T entity);
    }
}
