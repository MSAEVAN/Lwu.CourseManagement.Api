using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Room { get; set; }

        public ICollection<CourseClass> CourseClasses { get; set; } = new List<CourseClass>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
