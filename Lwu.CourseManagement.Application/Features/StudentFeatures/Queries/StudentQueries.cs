using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.StudentFeatures.Queries
{
    public static class StudentQueries
    {
        public record GetAll : IRequest<IReadOnlyList<StudentDto>>;
        public record GetAllClassCourses : IRequest<IReadOnlyList<StudentClassCoursesDto>>;
        public record GetByClass(Guid ClassId) : IRequest<IReadOnlyList<StudentDto>>;
        public record GetByCourse(Guid CourseId) : IRequest<IReadOnlyList<StudentDto>>;

        public class GetAllHandler(IUnitOfWork uow) : IRequestHandler<GetAll, IReadOnlyList<StudentDto>>
        {
            public async Task<IReadOnlyList<StudentDto>> Handle(GetAll request, CancellationToken ct)
            { 
                return await uow.StudentRepository.Filter(x => !x.IsDeleted).Select(s => new StudentDto(s.Id, s.FullName)).ToListAsync(ct);
            }
                
        }

        public class GetAllClassCoursesHandler(IUnitOfWork uow, ILoggedInUserService loggedInUser) : IRequestHandler<GetAllClassCourses, IReadOnlyList<StudentClassCoursesDto>>
        {
            public async Task<IReadOnlyList<StudentClassCoursesDto>> Handle(GetAllClassCourses request, CancellationToken ct)
            {
                if (loggedInUser.User.IsReadOnly)
                    return await (from cc in uow.CourseClassRepository.Filter(x => !x.IsDeleted)
                           join e in uow.EnrollmentRepository.Filter(x => x.Student.UserId == loggedInUser.Id)
                                on cc.ClassId equals e.ClassId
                           select new StudentClassCoursesDto(cc.Class.Name, cc.Course.Name)).ToListAsync(ct);
                else
                    return await uow.CourseClassRepository.Filter(x => !x.IsDeleted)
                    .Select(s => new StudentClassCoursesDto(s.Class.Name, s.Course.Name))
                    .ToListAsync(ct);
            }

        }

        public class GetByClassHandler(IUnitOfWork uow, ILoggedInUserService loggedInUser) : IRequestHandler<GetByClass, IReadOnlyList<StudentDto>>
        {
            public async Task<IReadOnlyList<StudentDto>> Handle(GetByClass request, CancellationToken ct)
            {
                if(loggedInUser.User.IsReadOnly)
                    return await uow.StudentRepository.Filter(x => !x.IsDeleted && x.UserId == loggedInUser.Id)
                        .Where(s => s.Enrollments.Any(e => !e.IsDeleted && e.ClassId == request.ClassId))
                        .Select(s => new StudentDto(s.Id, s.FullName))
                        .ToListAsync(ct);
                else
                    return await uow.StudentRepository.Filter(x => !x.IsDeleted)
                    .Where(s => s.Enrollments.Any(e => !e.IsDeleted && e.ClassId == request.ClassId))
                    .Select(s => new StudentDto(s.Id, s.FullName))
                    .ToListAsync(ct);
            }
        }

        public class GetByCourseHandler(IUnitOfWork uow) : IRequestHandler<GetByCourse, IReadOnlyList<StudentDto>>
        {
            public async Task<IReadOnlyList<StudentDto>> Handle(GetByCourse request, CancellationToken ct)
            {
                return await uow.StudentRepository.Filter(x=> !x.IsDeleted)
                    .Where(s => s.Enrollments.Any(e => !e.IsDeleted && e.Class.CourseClasses.Any(cc => !cc.IsDeleted && cc.CourseId == request.CourseId)))
                    .Select(s => new StudentDto(s.Id, s.FullName))
                    .ToListAsync(ct);
            }
        }
    }
}
