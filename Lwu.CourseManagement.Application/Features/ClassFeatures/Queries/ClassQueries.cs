using Lwu.CourseManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.ClassFeatures.Queries
{
    public static class ClassQueries
    {
        public record GetAll : IRequest<IReadOnlyList<ClassDto>>;
        public record GetByCourse(Guid CourseId) : IRequest<IReadOnlyList<ClassDto>>;

        public class GetAllHandler(IUnitOfWork uow) : IRequestHandler<GetAll, IReadOnlyList<ClassDto>>
        {
            public async Task<IReadOnlyList<ClassDto>> Handle(GetAll request, CancellationToken ct)
            {
                return await uow.ClassRepository.Filter(x=> !x.IsDeleted).Select(c => new ClassDto(c.Id, c.Name, c.Room)).ToListAsync(ct);
            }
        }

        public class GetByCourseHandler(IUnitOfWork uow) : IRequestHandler<GetByCourse, IReadOnlyList<ClassDto>>
        {
            public async Task<IReadOnlyList<ClassDto>> Handle(GetByCourse request, CancellationToken ct)
            {
                return await uow.ClassRepository.Filter(x=> !x.IsDeleted)
                    .Where(c => c.Id == request.CourseId)
                    .SelectMany(c => c.CourseClasses.Where(x=> !x.IsDeleted).Select(cc => cc.Class))
                    .Select(c => new ClassDto(c.Id, c.Name, c.Room))
                    .ToListAsync(ct);
            }
        }
    }
}
