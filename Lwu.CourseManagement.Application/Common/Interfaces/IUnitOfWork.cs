using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
using Lwu.CourseManagement.Application.Entities;
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
        //IRepository<Student> Students { get; }
        //IRepository<AppUser> Users { get; }

        //Task<int> SaveChangesAsync(CancellationToken ct = default);
        public IUserRepository UserRepository { get; }

        public int Commit();
        public Task<int> CommitAsync();
        public void DetachEntity<T>(T entity);
    }
}
