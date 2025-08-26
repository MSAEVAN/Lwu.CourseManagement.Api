using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Entities
{
    public class Staff : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;
    }
}
