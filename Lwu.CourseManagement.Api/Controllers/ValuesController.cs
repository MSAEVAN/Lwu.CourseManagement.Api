using Lwu.CourseManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILoggedInUserService _loggedInUserService;
        public ValuesController(ILoggedInUserService loggedInUser)
        {
            _loggedInUserService = loggedInUser;
        }

        [HttpGet]
        //[Authorize(Roles = "Editor")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetAll(CancellationToken ct)
        {
            var getUser = _loggedInUserService.User;
            return Ok(getUser);
        }
    }
}
