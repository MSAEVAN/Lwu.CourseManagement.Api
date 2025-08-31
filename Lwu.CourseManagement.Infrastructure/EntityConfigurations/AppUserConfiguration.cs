using Lwu.CourseManagement.Application.Entities;
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

            entityTypeBuilder
                .HasIndex(u => u.Username).IsUnique();

            entityTypeBuilder.Property(t => t.Email)
                .HasMaxLength(255)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Role)
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(t => t.UserPrincipal)
                .HasMaxLength(255)
                .IsRequired();

            entityTypeBuilder
                .HasOne(u => u.Student).WithOne(s => s.User).HasForeignKey<AppUser>(u => u.StudentId);

            entityTypeBuilder
                .HasOne(u => u.Staff).WithOne(s => s.User).HasForeignKey<AppUser>(u => u.StaffId);


            //entityTypeBuilder.HasOne(x => x.CreatedByUser).WithMany().OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
            //entityTypeBuilder.HasOne(x => x.ModifiedByUser).WithMany().OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.ModifiedByUserId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            //entityTypeBuilder.HasOne(x => x.DeletedByUser).WithMany().OnDelete(DeleteBehavior.Restrict).HasForeignKey(x => x.DeletedByUserId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        }
    }
}
