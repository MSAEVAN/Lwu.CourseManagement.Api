using Dapper;
using Lwu.CourseManagement.Application.Common.Interfaces.IDapper;
using Lwu.CourseManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.DapperServices
{
    public class StudentIdGenerator : IStudentIdGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection connection;
        public StudentIdGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
        }
        public async Task<string> GenerateStudentIdAsync()
        {
            // Example format: STU2025XXXX
            var year = DateTime.UtcNow.Year;

            string query = @"
                                SELECT COUNT(*) 
                                FROM ""Students"" 
                                WHERE EXTRACT(YEAR FROM ""CreatedOn"") = @Year;
                            ";

            await using var conn = connection;
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            var count = await conn.QueryFirstOrDefaultAsync<int>(query, new { Year = year });
            var nextNumber = count + 1;

            await conn.CloseAsync();

            return $"STU{year}{nextNumber:D4}";
        }
    }
}
