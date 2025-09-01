using Lwu.CourseManagement.Application.Features.UserFeatures.Dto;
using Lwu.CourseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces.IDapper
{
    public interface IAuthService
    {
        Task<AppUserDto> GetDetailByEmail(string email);
        Task<AppUserDto> GetDetailByUsername(string username);
    }
}
