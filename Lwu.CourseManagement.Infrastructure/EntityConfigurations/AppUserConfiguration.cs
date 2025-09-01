using Lwu.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Lwu.CourseManagement.Application.Features.UserFeatures.Command.CreateUserCommand;

namespace Lwu.CourseManagement.Infrastructure.EntityConfigurations
{
    public class AppUserConfiguration : BaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(t => t.FullName)
                .HasMaxLength(450)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Username)
                .HasMaxLength(255)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Email)
                .HasMaxLength(255)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Role)
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(t => t.UserPrincipal)
                .HasMaxLength(255)
                .IsRequired();

            // Indexes
            entityTypeBuilder.HasIndex(u => u.Email).IsUnique();
            entityTypeBuilder.HasIndex(u => u.Username).IsUnique();
        }
    }
}
