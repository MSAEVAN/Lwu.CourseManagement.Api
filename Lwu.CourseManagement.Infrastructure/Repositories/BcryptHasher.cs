using Lwu.CourseManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class BcryptHasher : IPasswordHasher
    {
        public string Hash(string input) => BCrypt.Net.BCrypt.HashPassword(input);
        public bool Verify(string input, string hash) => BCrypt.Net.BCrypt.Verify(input, hash);
    }
}
