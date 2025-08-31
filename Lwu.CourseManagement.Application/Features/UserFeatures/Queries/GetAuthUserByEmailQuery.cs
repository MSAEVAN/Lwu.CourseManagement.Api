using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.UserFeatures.Queries
{
    public class GetAuthUserByEmailQuery : IRequest<AppUser>
    {
        public string Email { get; set; }
        //public string Token { get; set; }
        public class GetAuthUserByEmailQueryHandler : IRequestHandler<GetAuthUserByEmailQuery, AppUser>
        {
            private readonly IAuthService _authService;
            public GetAuthUserByEmailQueryHandler(IAuthService authService)
            {
                _authService = authService;
            }
            public async Task<AppUser> Handle(GetAuthUserByEmailQuery query, CancellationToken cancellationToken)
            {
                return await _authService.GetDetailByEmail(query.Email);
            }
        }
    }
}
