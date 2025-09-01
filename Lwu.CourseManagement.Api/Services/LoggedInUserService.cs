using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Features.UserFeatures.Dto;
using Lwu.CourseManagement.Application.Features.UserFeatures.Queries;
using MediatR;
using System.Security.Claims;

namespace Lwu.CourseManagement.Api.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private AppUserDto _User { get; set; }
        public Guid Id
        {
            get
            {
                if (_User == null)
                {
                    LoadLoggedinuser();
                }
                return User.Id;
            }
        }

        public string Email { get { return User.Email; } }

        public AppUserDto User
        {
            get
            {

                if (_User == null)
                {
                    LoadLoggedinuser();
                }

                return _User;
            }
        }

        private void LoadLoggedinuser()
        {
            var email = _httpContextAccessor.HttpContext.User.Claims.Where(row => row.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Select(row => row.Value).SingleOrDefault();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0];
            var mediator = (IMediator)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMediator));
            _User = mediator.Send(new GetAuthUserByEmailQuery
            {
                Email = email,
                //Token = jwtToken
            }).Result;
        }

        public void LoadLoggedinuser(string email)
        {
            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0];
            var mediator = (IMediator)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMediator));
            _User = mediator.Send(new GetAuthUserByEmailQuery
            {
                Email = email,
                //Token = jwtToken
            }).Result;
        }

        public List<Claim> GetCustomClaims()
        {
            var claims = new List<Claim>();
            if (_User.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            if (!_User.IsReadOnly)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Editor"));
            }
            return claims;
        }
    }
}
