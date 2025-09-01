using Lwu.CourseManagement.Application.Common.Interfaces;
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
        public record GetByClass(Guid ClassId) : IRequest<IReadOnlyList<StudentDto>>;
        public record GetByCourse(Guid CourseId) : IRequest<IReadOnlyList<StudentDto>>;

        public class GetAllHandler(IUnitOfWork uow) : IRequestHandler<GetAll, IReadOnlyList<StudentDto>>
        {
            public async Task<IReadOnlyList<StudentDto>> Handle(GetAll request, CancellationToken ct)
            { 
                return await uow.StudentRepository.Filter(x => !x.IsDeleted).Select(s => new StudentDto(s.Id, s.FullName)).ToListAsync(ct);
            }
                
        }

        public class GetByClassHandler(IUnitOfWork uow) : IRequestHandler<GetByClass, IReadOnlyList<StudentDto>>
        {
            public async Task<IReadOnlyList<StudentDto>> Handle(GetByClass request, CancellationToken ct)
            {
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
