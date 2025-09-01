using Lwu.CourseManagement.Application;
using Lwu.CourseManagement.Application.Features.ClassFeatures.Queries;
using Lwu.CourseManagement.Application.Features.CourseFeatures.Command;
using Lwu.CourseManagement.Application.Features.CourseFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseApiController
    {
        [HttpGet]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await Mediator.Send(new CourseQueries.GetAll(), ct));
        }

        [HttpGet("by-class/{classId:guid}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetByCourse(Guid classId, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new CourseQueries.GetCourseByClass(classId), ct));
        }

        [HttpPost]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<CourseDto>> Create([FromBody] CreateCourseRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new CourseCommands.Create(request), ct));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<CourseDto>> Update(Guid id, [FromBody] UpdateCourseRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new CourseCommands.Update(id, request), ct));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await Mediator.Send(new CourseCommands.Delete(id), ct);
            return NoContent();
        }
    }
}
