using Lwu.CourseManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.CourseFeatures.Queries
{
    public static class CourseQueries
    {
        public record GetAll : IRequest<IReadOnlyList<CourseDto>>;
        public class Handler(IUnitOfWork uow) : IRequestHandler<GetAll, IReadOnlyList<CourseDto>>
        {
            public async Task<IReadOnlyList<CourseDto>> Handle(GetAll request, CancellationToken ct)
            {
                return await uow.CourseRepository.Filter(x=> !x.IsDeleted)
                    .Select(c => new CourseDto(c.Id, c.Name, c.Description))
                    .ToListAsync(ct);
            }
        }
    }
}
