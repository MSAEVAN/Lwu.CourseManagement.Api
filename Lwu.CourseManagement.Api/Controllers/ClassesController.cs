using Lwu.CourseManagement.Application;
using Lwu.CourseManagement.Application.Features.ClassFeatures.Command;
using Lwu.CourseManagement.Application.Features.ClassFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lwu.CourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClassDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await Mediator.Send(new ClassQueries.GetAll(), ct));
        }

        [HttpGet("by-course/{courseId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ClassDto>>> GetByCourse(Guid courseId, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new ClassQueries.GetByCourse(courseId), ct));
        }

        [HttpPost]
        //[Authorize(Roles = "Staff")]
        public async Task<ActionResult<ClassDto>> Create([FromBody] CreateClassRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new ClassCommands.Create(request), ct));
        }

        [HttpPut("{id:guid}")]
        //[Authorize(Roles = "Staff")]
        public async Task<ActionResult<ClassDto>> Update(Guid id, [FromBody] UpdateClassRequest request, CancellationToken ct)
        {
            return Ok(await Mediator.Send(new ClassCommands.Update(id, request), ct));
        }

        [HttpDelete("{id:guid}")]
        //[Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await Mediator.Send(new ClassCommands.Delete(id), ct);
            return NoContent();
        }
    }
}
