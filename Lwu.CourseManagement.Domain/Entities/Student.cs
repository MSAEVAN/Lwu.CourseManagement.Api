using Lwu.CourseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string ID { get; set; } = default!;
        public string FullName { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
