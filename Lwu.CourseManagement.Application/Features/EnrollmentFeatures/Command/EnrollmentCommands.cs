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

namespace Lwu.CourseManagement.Application.Features.EnrollmentFeatures.Command
{
    public static class EnrollmentCommands
    {
        public record EnrollStudentInClass(Guid StudentId, Guid ClassId, Guid AssignedByUserId) : IRequest<CustomResponse<Unit>>;
        public record EnrollStudentInCourse(Guid StudentId, Guid CourseId, Guid AssignedByUserId) : IRequest<CustomResponse<Unit>>;

        public class EnrollInClassHandler(IUnitOfWork uow) : IRequestHandler<EnrollStudentInClass, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(EnrollStudentInClass request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var exists = await uow.EnrollmentRepository.Filter(x=> !x.IsDeleted)
                    .AnyAsync(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId, ct);
                if (!exists)
                {
                    uow.EnrollmentRepository.Insert(new Enrollment
                    {
                        StudentId = request.StudentId,
                        ClassId = request.ClassId,
                        AssignedByUserId = request.AssignedByUserId,
                        AssignedAt = DateTime.UtcNow
                    });
                    await uow.CommitAsync();

                    response.Data = Unit.Value;
                    response.IsSucceed = true;
                    response.Message = "Student successfully enrolled in class.";
                }
                else
                {
                    response.Data = Unit.Value;
                    response.IsSucceed = true;
                    response.Message = "Student already enrolled in class.";
                }

                return response;
            }
        }

        public class EnrollInCourseHandler(IUnitOfWork uow) : IRequestHandler<EnrollStudentInCourse, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(EnrollStudentInCourse request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var classIds = await uow.CourseRepository.Filter(x=> !x.IsDeleted && x.Id == request.CourseId)
                    .SelectMany(c => c.CourseClasses.Where(x=> !x.IsDeleted).Select(cc => cc.ClassId))
                    .ToListAsync(ct);

                foreach (var classId in classIds)
                {
                    var exists = await uow.EnrollmentRepository.Filter(x=> !x.IsDeleted)
                        .AnyAsync(e => e.StudentId == request.StudentId && e.ClassId == classId, ct);
                    if (!exists)
                    {
                        uow.EnrollmentRepository.Insert(new Enrollment
                        {
                            StudentId = request.StudentId,
                            ClassId = classId,
                            AssignedByUserId = request.AssignedByUserId,
                            AssignedAt = DateTime.UtcNow
                        });
                    }
                }
                await uow.CommitAsync();

                response.Data = Unit.Value;
                response.IsSucceed = true;
                response.Message = "Student successfully enrolled in course.";

                return response;
            }
        }
    }
}
