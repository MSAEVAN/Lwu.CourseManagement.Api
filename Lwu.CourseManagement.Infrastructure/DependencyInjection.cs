using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Domain;
using Lwu.CourseManagement.Infrastructure.DAL;
using Lwu.CourseManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IAuthService), typeof(AuthService));

            return services;
        }
    }
}
