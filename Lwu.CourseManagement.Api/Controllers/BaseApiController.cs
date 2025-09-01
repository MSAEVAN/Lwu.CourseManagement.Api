using Lwu.CourseManagement.Api.Attributes;
using Lwu.CourseManagement.Application.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Authorize]
    //[AppAuthorizeUpto(UserRole.User)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
