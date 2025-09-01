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
    public class CourseConfiguration : BaseEntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(t => t.Name)
                .HasMaxLength(128)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Description)
                .HasMaxLength(1000);

            // Relationship: Course -> CourseClasses (one-to-many)
            entityTypeBuilder.HasMany(c => c.CourseClasses)
                   .WithOne(cc => cc.Course) // assuming CourseClass has a Course navigation property
                   .HasForeignKey(cc => cc.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Add indexes if needed
            entityTypeBuilder.HasIndex(c => c.Name)
                   .IsUnique(); // optional, only if course names must be unique
        }
    }
}
