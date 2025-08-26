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
        IRepository<Student> Students { get; }
        IRepository<AppUser> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
