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
    public class StudentConfiguration : BaseEntityConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(200);

            entityTypeBuilder.Property(t => t.ID)
                .HasMaxLength(100)
                .IsRequired();

            entityTypeBuilder.Property(s => s.ID)
                    .HasDefaultValueSql("concat('STU', date_part('year', now())::int, lpad(nextval('student_id_seq')::text, 4, '0'))");

            // Relationships
            entityTypeBuilder.HasOne(s => s.User)
                    .WithMany() // or .WithOne() if AppUser has Student navigation
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.HasMany(s => s.Enrollments)
                    .WithOne(e => e.Student)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Index for UserId for faster lookups
            entityTypeBuilder.HasIndex(s => s.UserId)
                .IsUnique(); // Only if each AppUser can have one Student record
        }
    }
}
