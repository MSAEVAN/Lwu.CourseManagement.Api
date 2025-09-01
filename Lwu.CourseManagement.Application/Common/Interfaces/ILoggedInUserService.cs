using Lwu.CourseManagement.Application.Features.UserFeatures.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces
{
    public interface ILoggedInUserService
    {
        Guid Id { get; }
        string Email { get; }
        AppUserDto User { get; }
        void LoadLoggedinuser(string email);
        public List<Claim> GetCustomClaims();
    }
}
