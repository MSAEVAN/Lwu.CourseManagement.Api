using Lwu.CourseManagement.Application;
using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Interfaces.IRepositories;
using Lwu.CourseManagement.Infrastructure.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IConfiguration config, IUnitOfWork unitOfWork, IPasswordHasher hasher)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] Application.LoginRequest request, CancellationToken ct)
        {
            var user = await _unitOfWork.UserRepository.Filter(u => u.Username == request.Username).FirstOrDefaultAsync();
            if (user is null || !_hasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                },
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"]!)),
                signingCredentials: creds);

            return Ok(new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token)));
        }
    }
}
