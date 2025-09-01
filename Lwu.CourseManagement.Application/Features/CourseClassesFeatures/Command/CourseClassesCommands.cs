using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Responses;
using Lwu.CourseManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.CourseClassesFeatures.Command
{
    public static class CourseClassesCommands
    {
        public record CourseClassesLink(Guid courseId, Guid classId) : IRequest<CustomResponse<Unit>>;
        public record CourseClassesUnlink(Guid courseId, Guid classId) : IRequest<CustomResponse<Unit>>;

        public class CourseClassesLinkHandler(IUnitOfWork uow) : IRequestHandler<CourseClassesLink, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(CourseClassesLink request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var courseEntity = await uow.CourseRepository.Filter(x => !x.IsDeleted && x.Id == request.courseId).FirstOrDefaultAsync();
                var classEntity = await uow.ClassRepository.Filter(x => !x.IsDeleted && x.Id == request.classId).FirstOrDefaultAsync();

                if (courseEntity != null && classEntity != null)
                {
                    var exists = await uow.CourseClassRepository.Filter(x => !x.IsDeleted).AnyAsync(cc => cc.CourseId == request.courseId && cc.ClassId == request.classId, ct);
                    if (!exists)
                    {
                        uow.CourseClassRepository.Insert(new CourseClass { CourseId = courseEntity.Id, ClassId = classEntity.Id });
                        await uow.CommitAsync();

                        response.Data = Unit.Value;
                        response.IsSucceed = true;
                        response.Message = "Course-Classes Linked successfully.";
                    }
                    else
                    {
                        response.Data = Unit.Value;
                        response.IsSucceed = false;
                        response.Message = "Course-Classes Link already exist.";
                    }

                }
                else
                {
                    response.IsSucceed = false;
                    response.Message = "Course/Class not found";
                }

                return response;
            }
        }

        public class CourseClassesUnlinkHandler(IUnitOfWork uow) : IRequestHandler<CourseClassesUnlink, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(CourseClassesUnlink request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var courseClassEntity = await uow.CourseClassRepository.Filter(x => !x.IsDeleted && x.CourseId == request.courseId && x.ClassId == request.classId).FirstOrDefaultAsync();
                
                if (courseClassEntity is not null)
                {
                    // Hard Delete
                    // uow.CourseClassRepository.Delete(courseClassEntity);

                    // Soft Delete
                    courseClassEntity.IsDeleted = true;
                    courseClassEntity.DeletedOn = DateTime.UtcNow;
                    uow.CourseClassRepository.Update(courseClassEntity);
                    await uow.CommitAsync();

                    response.Data = Unit.Value;
                    response.IsSucceed = true;
                    response.Message = "Course-Classes Unlinked successfully.";

                }
                else
                {
                    response.IsSucceed = false;
                    response.Message = "Course-Class link not found";
                }

                return response;
            }
        }
    }
}
