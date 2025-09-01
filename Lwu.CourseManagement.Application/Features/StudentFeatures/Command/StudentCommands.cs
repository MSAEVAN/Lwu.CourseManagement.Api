using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Interfaces.IDapper;
using Lwu.CourseManagement.Application.Common.Responses;
using Lwu.CourseManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.StudentFeatures.Command
{
    public static class StudentCommands
    {
        public record Create(CreateStudentRequest Request) : IRequest<CustomResponse<StudentDto>>;
        public record Update(Guid Id, UpdateStudentRequest Request) : IRequest<CustomResponse<StudentDto>>;
        public record Delete(Guid Id) : IRequest<CustomResponse<Unit>>;

        public class CreateHandler(IUnitOfWork uow, IPasswordHasher hasher, IStudentIdGenerator idGenerator) : IRequestHandler<Create, CustomResponse<StudentDto>>
        {
            public async Task<CustomResponse<StudentDto>> Handle(Create request, CancellationToken ct)
            {
                CustomResponse<StudentDto> response = new CustomResponse<StudentDto>();
                // Validation Checking
                var isNotValid = uow.UserRepository.Filter(x => !x.IsDeleted && (x.Username.ToLower() == request.Request.Username.ToLower() || x.Email.ToLower() == request.Request.Email.ToLower())).Any();
                if (isNotValid)
                {
                    response.Message = "Username or Email already exist!";
                    response.IsSucceed = false;
                    return response;
                }
                //

                var studentRes = new Student();
                var user = new AppUser
                {
                    FullName = request.Request.FullName,
                    Username = request.Request.Username,
                    Email = request.Request.Email,
                    UserPrincipal = "Student",
                    PasswordHash = hasher.Hash(request.Request.Password),
                    Role = "Student"
                };
                var saveUser = uow.UserRepository.Insert(user);
                if (saveUser != null) 
                {
                    var getSid = await idGenerator.GenerateStudentIdAsync();

                    var student = new Student { ID = getSid, FullName = request.Request.FullName, UserId = saveUser.Id };
                    studentRes = uow.StudentRepository.Insert(student);

                    await uow.CommitAsync();
                }
                else
                {
                    response.Message = "Save failed, no rows affected.";
                    response.IsSucceed = false;
                    return response;
                }

                response.Data = new StudentDto(studentRes.Id, studentRes.FullName);
                response.IsSucceed = true;
                response.Message = "Student saved successfully.";

                return response;
            }
        }

        public class UpdateHandler(IUnitOfWork uow) : IRequestHandler<Update, CustomResponse<StudentDto>>
        {
            public async Task<CustomResponse<StudentDto>> Handle(Update request, CancellationToken ct)
            {
                CustomResponse<StudentDto> response = new CustomResponse<StudentDto>();
                var student = await uow.StudentRepository.Filter(x=> !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                
                if(student == null)
                {
                    response.Message = "Student not found!";
                    response.IsSucceed = false;
                    return response;
                }
                
                student.FullName = request.Request.FullName;
                student.ModifiedOn = DateTime.UtcNow;
                uow.StudentRepository.Update(student);
                await uow.CommitAsync();

                response.Data = new StudentDto(student.Id, student.FullName);
                response.IsSucceed = true;
                response.Message = "Student updated successfully";

                return response;
            }
        }

        public class DeleteHandler(IUnitOfWork uow) : IRequestHandler<Delete, CustomResponse<Unit>>
        {
            public async Task<CustomResponse<Unit>> Handle(Delete request, CancellationToken ct)
            {
                CustomResponse<Unit> response = new CustomResponse<Unit>();
                var student = await uow.StudentRepository.Filter(x=> !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                if (student == null)
                {
                    response.Message = "Student not found!";
                    response.IsSucceed = false;
                    return response;
                }

                var studentUser = await uow.UserRepository.Filter(x => !x.IsDeleted && student.UserId == x.Id).FirstOrDefaultAsync();
                if (studentUser == null)
                {
                    response.Message = "Student as user not found!";
                    response.IsSucceed = false;
                    return response;
                }

                // Hard Delete
                // uow.UserRepository.Delete(studentUser);
                // uow.StudentRepository.Delete(student); 
                // end

                // Soft Delete
                studentUser.IsDeleted = true;
                studentUser.DeletedOn = DateTime.UtcNow;
                uow.UserRepository.Update(studentUser);

                student.IsDeleted = true;
                student.DeletedOn = DateTime.UtcNow;
                uow.StudentRepository.Update(student);
                // end

                await uow.CommitAsync();

                response.Data = Unit.Value;
                response.IsSucceed = true;
                response.Message = "Student deleted successfully";

                return response;
            }
        }
    }
}
