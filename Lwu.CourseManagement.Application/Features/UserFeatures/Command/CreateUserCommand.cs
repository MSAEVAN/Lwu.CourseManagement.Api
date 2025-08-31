using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Common.Responses;
using Lwu.CourseManagement.Application.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Features.UserFeatures.Command
{
    public class CreateUserCommand 
    {
        public record Create(CreateUserResponse Request): IRequest<CustomResponse<bool>>;

        public class CreateUserCommandHandler : IRequestHandler<Create, CustomResponse<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;


            public CreateUserCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

            }

            public async Task<CustomResponse<bool>> Handle(Create command, CancellationToken cancellationToken)
            {
                CustomResponse<bool> response = new CustomResponse<bool>();
                //check if user exists
                var userInfo = await _unitOfWork.UserRepository.GetSingleAsync(a => !a.IsDeleted && a.Id == command.Request.Id);

                var uId = string.IsNullOrEmpty(command.Request.Id.ToString()) ?  Guid.NewGuid() : command.Request.Id;

                var user = new AppUser();
                user.Id = uId;
                user.Username = command.Request.UserName;
                user.FullName = command.Request.FullName;
                user.Email = command.Request.Email;
                user.Role = "Staff";
                user.UserPrincipal = command.Request.Email;
                user.IsDeleted = false;
                user.CreatedByUserId = uId;
                user.CreatedOn = System.DateTime.Now;

                if (userInfo != null)
                {
                    _unitOfWork.UserRepository.Update(user);
                }
                else
                {
                    _unitOfWork.UserRepository.Insert(user);
                }

                await _unitOfWork.CommitAsync();

                response.Data = true;
                response.IsSucceed = true;
                return response;
            }
        }

    }
}
