using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public Guid ClassId { get; set; }
        public Class Class { get; set; } = default!;

        public Guid AssignedByUserId { get; set; }
        public AppUser? AssignedBy { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
