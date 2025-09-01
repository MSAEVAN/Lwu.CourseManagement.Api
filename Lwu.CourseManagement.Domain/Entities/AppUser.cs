using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public string Username { get; set; } = default!;

        public string Email { get; set; } = string.Empty;

        public string UserPrincipal { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = "Staff"; // "Staff" | "Student"
    }
}
