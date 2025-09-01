using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
using Lwu.CourseManagement.Domain.Entities;
using Lwu.CourseManagement.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {


        }
    }
}
