using Lwu.CourseManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.CourseClassesFeatures.Queries
{
    public static class CourseClassesQueries
    {
        public record GetAll() : IRequest<IReadOnlyList<StudentClassDto>>;
        public class GetAllHandler(IUnitOfWork uow) : IRequestHandler<GetAll, IReadOnlyList<StudentClassDto>>
        {
            public async Task<IReadOnlyList<StudentClassDto>> Handle(GetAll request, CancellationToken ct)
            {
                return await uow.EnrollmentRepository.Filter(x => !x.IsDeleted)
                    .Select(c => new StudentClassDto(c.Student.FullName, c.Class.Name, c.AssignedBy.FullName, c.AssignedAt)).ToListAsync(ct);
            }
        }
    }
}
