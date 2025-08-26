using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string input);
        bool Verify(string input, string hash);
    }
}
