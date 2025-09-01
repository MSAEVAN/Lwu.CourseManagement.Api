using Dapper;
using Lwu.CourseManagement.Application.Common.Interfaces.IDapper;
using Lwu.CourseManagement.Application.Features.UserFeatures.Dto;
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
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection connection;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
        }

        public async Task<AppUserDto> GetDetailByEmail(string email)
        {
            const string query = @"
                                    SELECT * 
                                    FROM ""AppUsers"" 
                                    WHERE ""IsDeleted"" = FALSE 
                                      AND ""Email"" = @Email;
                                ";

            await using var conn = connection;
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();
            
            var getUser = await conn.QueryFirstOrDefaultAsync<AppUser>(query, new { Email = email });

            var userRes = ConvertToAppUserDto(getUser);

            await conn.CloseAsync();
            return userRes;
        }

        public async Task<AppUserDto> GetDetailByUsername(string username)
        {
            const string query = @"
                                    SELECT * 
                                    FROM ""AppUsers"" 
                                    WHERE ""IsDeleted"" = FALSE 
                                      AND ""UserName"" = @Email;
                                ";

            await using var conn = connection;
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            var getUser = await conn.QueryFirstOrDefaultAsync<AppUser>(query, new { UserName = username });

            var userRes = ConvertToAppUserDto(getUser);

            await conn.CloseAsync();
            return userRes;
        }

        private AppUserDto ConvertToAppUserDto(AppUser getUser)
        {
            var userRes = new AppUserDto();
            if (getUser != null)
            {
                userRes.FullName = getUser.FullName;
                userRes.Email = getUser.Email;
                userRes.Username = getUser.Username;
                userRes.IsReadOnly = getUser.Role.ToLower() == "stuff" ? false : true;
                userRes.Id = getUser.Id;
                userRes.IsAdmin = getUser.UserPrincipal.ToLower() == "admin" ? true : false;
            }
            return userRes;
        }
    }
}
