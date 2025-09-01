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

namespace Lwu.CourseManagement.Application.Features.ClassFeatures.Command
{
    public static class ClassCommands
    {
        public record Create(CreateClassRequest Request) : IRequest<CustomResponse<ClassDto>>;
        public record Update(Guid Id, UpdateClassRequest Request) : IRequest<CustomResponse<ClassDto>>;
        public record Delete(Guid Id) : IRequest<CustomResponse<Unit>>;
        public record LinkToCourse(Guid CourseId, Guid ClassId) : IRequest<CustomResponse<Unit>>;
        public record UnlinkFromCourse(Guid CourseId, Guid ClassId) : IRequest<CustomResponse<Unit>>;

        public class CreateHandler(IUnitOfWork uow) : IRequestHandler<Create, CustomResponse<ClassDto>>
        {
            public async Task<CustomResponse<ClassDto>> Handle(Create request, CancellationToken ct)
            {
                CustomResponse<ClassDto> response = new CustomResponse<ClassDto>();
                // Validation Checking
                var isNotValid = uow.ClassRepository.Filter(x=> !x.IsDeleted && x.Name.ToLower() == request.Request.Name.ToLower()).Any();
                if (isNotValid)
                {
                    response.Message = "Class already exist!";
                    response.IsSucceed = false;
                    return response;
                }
                //

                var entity = new Class { Name = request.Request.Name, Room = request.Request.Room };
                var saveClass = uow.ClassRepository.Insert(entity);
                await uow.CommitAsync();

                response.Data = new ClassDto(saveClass.Id, saveClass.Name, saveClass.Room);
                response.IsSucceed = true;
                response.Message = "Class saved successfully.";


                return response;
            }
        }

        public class UpdateHandler(IUnitOfWork uow) : IRequestHandler<Update, CustomResponse<ClassDto>>
        {
            public async Task<CustomResponse<ClassDto>> Handle(Update request, CancellationToken ct)
            {
                CustomResponse<ClassDto> response = new CustomResponse<ClassDto>();

                var classEntity = await uow.ClassRepository.Filter(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                if (classEntity == null)
                {
                    response.Message = "Class not found!";
                    response.IsSucceed = false;
                    return response;
                }

                // Validation Checking
                var isNotValid = uow.ClassRepository.Filter(x => !x.IsDeleted && x.Id != classEntity.Id && x.Name.ToLower() == request.Request.Name.ToLower()).Any();
                if (isNotValid)
                {
                    response.Message = "Class already exist!";
                    response.IsSucceed = false;
                    return response;
                }
                //

                classEntity.Name = request.Request.Name;
                classEntity.Room = request.Request.Room;
                classEntity.ModifiedOn = DateTime.UtcNow;
                uow.ClassRepository.Update(classEntity);
                await uow.CommitAsync();

                response.Data = new ClassDto(classEntity.Id, classEntity.Name, classEntity.Room);
                response.IsSucceed = true;
                response.Message = "Class updated successfully";

                return response;
            }
        }

        public class DeleteHandler(IUnitOfWork uow) : IRequestHandler<Delete, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(Delete request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();

                var classEntity = await uow.ClassRepository.Filter(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                if (classEntity == null)
                {
                    response.Message = "Class not found!";
                    response.IsSucceed = false;
                    return response;
                }

                // Hard Delete
                //uow.ClassRepository.Delete(classEntity);

                // Soft Delete
                classEntity.IsDeleted = true;
                classEntity.DeletedOn = DateTime.UtcNow;
                uow.ClassRepository.Update(classEntity);

                await uow.CommitAsync();

                response.Data = Unit.Value;
                response.IsSucceed = true;
                response.Message = "Class deleted successfully";

                return response;
            }
        }
    }
}
