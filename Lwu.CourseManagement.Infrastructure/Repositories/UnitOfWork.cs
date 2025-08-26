using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Entities;
using Lwu.CourseManagement.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
    {
        public IRepository<Student> Students => new EfRepository<Student>(db);
        public IRepository<AppUser> Users => new EfRepository<AppUser>(db);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
    }
}
