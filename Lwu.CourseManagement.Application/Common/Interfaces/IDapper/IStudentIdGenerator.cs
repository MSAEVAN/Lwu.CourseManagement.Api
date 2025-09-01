using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces.IDapper
{
    public interface IStudentIdGenerator
    {
        public Task<string> GenerateStudentIdAsync();
    }
}
