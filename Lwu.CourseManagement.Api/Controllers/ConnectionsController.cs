using Lwu.CourseManagement.Application;
using Lwu.CourseManagement.Application.Features.CourseClassesFeatures.Command;
using Lwu.CourseManagement.Application.Features.EnrollmentFeatures.Command;
using Lwu.CourseManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : BaseApiController
    {
        [HttpPost("courses/{courseId:guid}/classes/{classId:guid}")]
        public async Task<IActionResult> Link(Guid courseId, Guid classId, CancellationToken ct)
        {
            var res = await Mediator.Send(new CourseClassesCommands.CourseClassesLink(courseId, classId), ct);
            return Ok(res);
        }

        [HttpDelete("courses/{courseId:guid}/classes/{classId:guid}")]
        public async Task<IActionResult> Unlink(Guid courseId, Guid classId, CancellationToken ct)
        {
            var res = await Mediator.Send(new CourseClassesCommands.CourseClassesUnlink(courseId, classId), ct);
            return Ok(res);
        }

        [HttpPost("enroll/class")]
        public async Task<IActionResult> EnrollInClass([FromBody] EnrollClassRequest req, CancellationToken ct)
        {
            var staffId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var res = await Mediator.Send(new EnrollmentCommands.EnrollStudentInClass(req.StudentId, req.ClassId, staffId), ct);
            return Ok(res);
        }

        [HttpPost("enroll/course")]
        public async Task<IActionResult> EnrollInCourse([FromBody] EnrollCourseRequest req, CancellationToken ct)
        {
            var staffId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var res = await Mediator.Send(new EnrollmentCommands.EnrollStudentInCourse(req.StudentId, req.CourseId, staffId), ct);
            return Ok(res);
        }
    }
}
