﻿using Lwu.CourseManagement.Application.Common.Enums;
using Lwu.CourseManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lwu.CourseManagement.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AppAuthorizeUpto : ActionFilterAttribute
    {

        public AppAuthorizeUpto(UserRole role) : base()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.Filters.Any(row => row is AllowAnonymousFilter) &&
                    !(context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo.GetCustomAttributes(true).Any(row => row is AllowAnonymousAttribute) == true
                    )
                {
                    if (context.HttpContext.User.Identity.IsAuthenticated)
                    {

                        var loggedInUserService = (ILoggedInUserService)context.HttpContext.RequestServices.GetService(typeof(ILoggedInUserService));
                        var user = loggedInUserService.User;

                        if (user != null)
                        {
                            // User Authorized Successfully
                        }
                        else
                        {
                            context.Result = new UnauthorizedResult();
                        }
                    }
                    else
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
            catch (Exception)
            {
                context.Result = new BadRequestResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
