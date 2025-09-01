using Lwu.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.EntityConfigurations
{
    public class EnrollmentConfiguration : BaseEntityConfiguration<Enrollment>
    {
        public override void Configure(EntityTypeBuilder<Enrollment> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            // Student relationship (required)
            entityTypeBuilder.HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Class relationship (required)
            entityTypeBuilder.HasOne(e => e.Class)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.ClassId)
                   .OnDelete(DeleteBehavior.Cascade);

            // AssignedBy relationship (optional)
            entityTypeBuilder.HasOne(e => e.AssignedBy)
                   .WithMany() // Assuming AppUser can assign many enrollments, but not mapped in AppUser
                   .HasForeignKey(e => e.AssignedByUserId)
                   .OnDelete(DeleteBehavior.Restrict) // Prevent cascade delete on users
                   .IsRequired(false);

            // AssignedAt default value
            entityTypeBuilder.Property(e => e.AssignedAt)
                   .HasDefaultValueSql("NOW()"); // PostgreSQL: sets default value to current UTC time
        }
    }
}
