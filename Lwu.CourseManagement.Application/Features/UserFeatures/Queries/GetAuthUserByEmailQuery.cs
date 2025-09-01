using Lwu.CourseManagement.Application.Common.Interfaces.IDapper;
using Lwu.CourseManagement.Application.Features.UserFeatures.Dto;
using Lwu.CourseManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.UserFeatures.Queries
{
    public class GetAuthUserByEmailQuery : IRequest<AppUserDto>
    {
        public string Email { get; set; }
        //public string Token { get; set; }
        public class GetAuthUserByEmailQueryHandler : IRequestHandler<GetAuthUserByEmailQuery, AppUserDto>
        {
            private readonly IAuthService _authService;
            public GetAuthUserByEmailQueryHandler(IAuthService authService)
            {
                _authService = authService;
            }
            public async Task<AppUserDto> Handle(GetAuthUserByEmailQuery query, CancellationToken cancellationToken)
            {
                return await _authService.GetDetailByEmail(query.Email);
            }
        }
    }
}
