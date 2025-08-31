using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application
{
    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string Token);

    public record CreateUserResponse(Guid Id, string FullName, string UserName, string Email, string UserPrincipal, string Role, string PasswordHash);
}
