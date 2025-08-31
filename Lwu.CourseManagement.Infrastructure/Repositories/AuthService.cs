using Dapper;
using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection connection;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<AppUser> GetDetailByEmail(string email)
        {
            connection.Open();
            string query = "SELECT * FROM ApplicationUser WHERE IsDeleted = 0 AND email = @Email";
            var result = await connection.QueryAsync<AppUser>(query, new { Email = email });
            connection.Close();
            return result.FirstOrDefault();
        }
    }
}
