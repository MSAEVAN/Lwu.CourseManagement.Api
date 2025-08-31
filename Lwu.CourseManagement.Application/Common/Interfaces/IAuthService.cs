using Lwu.CourseManagement.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AppUser> GetDetailByEmail(string email);
    }
}
