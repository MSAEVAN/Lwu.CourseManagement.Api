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

namespace Lwu.CourseManagement.Application.Features.CourseFeatures.Command
{
    public static class CourseCommands
    {
        public record Create(CreateCourseRequest Request) : IRequest<CustomResponse<CourseDto>>;
        public record Update(Guid Id, UpdateCourseRequest Request) : IRequest<CustomResponse<CourseDto>>;
        public record Delete(Guid Id) : IRequest<CustomResponse<Unit>>;

        public class CreateHandler(IUnitOfWork uow) : IRequestHandler<Create, CustomResponse<CourseDto>>
        {
            public async Task<CustomResponse<CourseDto>> Handle(Create request, CancellationToken ct)
            {
                CustomResponse<CourseDto> response = new CustomResponse<CourseDto>();
                // Validation Checking
                var isNotValid = uow.CourseRepository.Filter(x => !x.IsDeleted && x.Name.ToLower() == request.Request.Name.ToLower()).Any();
                if (isNotValid)
                {
                    response.Message = "Course already exist!";
                    response.IsSucceed = false;
                    return response;
                }
                //

                var entity = new Course { Name = request.Request.Name, Description = request.Request.Description };
                var courseEntity = uow.CourseRepository.Insert(entity);
                await uow.CommitAsync();

                response.Data = new CourseDto(courseEntity.Id, courseEntity.Name, courseEntity.Description);
                response.IsSucceed = true;
                response.Message = "Course saved successfully.";

                return response;
            }
        }

        public class UpdateHandler(IUnitOfWork uow) : IRequestHandler<Update, CustomResponse<CourseDto>>
        {
            public async Task<CustomResponse<CourseDto>> Handle(Update request, CancellationToken ct)
            {
                CustomResponse<CourseDto> response = new CustomResponse<CourseDto>();

                var courseEntity = await uow.CourseRepository.Filter(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                if (courseEntity == null)
                {
                    response.Message = "Course not found!";
                    response.IsSucceed = false;
                    return response;
                }

                // Validation Checking
                var isNotValid = uow.CourseRepository.Filter(x => !x.IsDeleted && x.Id != courseEntity.Id && x.Name.ToLower() == request.Request.Name.ToLower()).Any();
                if (isNotValid)
                {
                    response.Message = "Course already exist!";
                    response.IsSucceed = false;
                    return response;
                }
                //

                courseEntity.Name = request.Request.Name;
                courseEntity.Description = request.Request.Description;
                courseEntity.ModifiedOn = DateTime.UtcNow;
                uow.CourseRepository.Update(courseEntity);
                await uow.CommitAsync();

                response.Data = new CourseDto(courseEntity.Id, courseEntity.Name, courseEntity.Description);
                response.IsSucceed = true;
                response.Message = "Course updated successfully";

                return response;
            }
        }

        public class DeleteHandler(IUnitOfWork uow) : IRequestHandler<Delete, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(Delete request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var courseEntity = await uow.CourseRepository.Filter(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                if (courseEntity == null)
                {
                    response.Message = "Course not found!";
                    response.IsSucceed = false;
                    return response;
                }

                // Hard Delete
                //uow.CourseRepository.Delete(courseEntity);

                // Soft Delete
                courseEntity.IsDeleted = true;
                courseEntity.DeletedOn = DateTime.UtcNow;
                uow.CourseRepository.Update(courseEntity);

                await uow.CommitAsync();

                response.Data = Unit.Value;
                response.IsSucceed = true;
                response.Message = "Course deleted successfully";

                return response;
            }
        }
    }
}
