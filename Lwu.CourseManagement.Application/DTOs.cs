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


    //
    public record StudentDto(Guid Id, string FullName);
    public record CourseDto(Guid Id, string Name, string? Description);
    public record ClassDto(Guid Id, string Name, string? Room);
    public record UserDto(Guid Id, string Username, string Role);

    public record CreateCourseRequest(string Name, string? Description);
    public record UpdateCourseRequest(string Name, string? Description);

    public record CreateClassRequest(string Name, string? Room);
    public record UpdateClassRequest(string Name, string? Room);

    public record CreateStudentRequest(string FullName, string Email, string Username, string Password);
    public record UpdateStudentRequest(string FullName);
}
