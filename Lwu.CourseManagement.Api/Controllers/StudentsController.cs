using Lwu.CourseManagement.Application;
using Lwu.CourseManagement.Application.Features.StudentFeatures.Command;
using Lwu.CourseManagement.Application.Features.StudentFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lwu.CourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class StudentsController : BaseApiController
    {
        [HttpGet]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentQueries.GetAll(), ct));
        }

        [HttpGet("get-all-class-courses")]
        [Authorize(Policy = "StudentOrStaff")]
        public async Task<ActionResult<IReadOnlyList<StudentClassCoursesDto>>> GetAllClassCourses(CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentQueries.GetAllClassCourses(), ct));
        }

        [HttpGet("by-class/{classId:guid}")]
        [Authorize(Policy = "StudentOrStaff")]
        public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetByClass(Guid classId, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentQueries.GetByClass(classId), ct));
        }

        [HttpGet("by-course/{courseId:guid}")]
        [Authorize(Policy = "StudentOrStaff")]
        public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetByCourse(Guid courseId, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentQueries.GetByCourse(courseId), ct));
        }

        [HttpPost]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentCommands.Create(request), ct));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<ActionResult<StudentDto>> Update(Guid id, [FromBody] UpdateStudentRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new StudentCommands.Update(id, request), ct));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await Mediator.Send(new StudentCommands.Delete(id), ct);
            return NoContent();
        }
    }
}
