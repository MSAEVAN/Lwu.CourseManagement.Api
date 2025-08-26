using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Entities
{
    public class AppUser : BaseEntity
    {
        [Required, MaxLength(64)]
        public string Username { get; set; } = default!;

        public string Email { get; set; } = string.Empty;

        public string UserPrincipal { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required, MaxLength(32)]
        public string Role { get; set; } = "Staff"; // "Staff" | "Student"

        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
