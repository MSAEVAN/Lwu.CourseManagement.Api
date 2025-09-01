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
    public class CourseClassConfiguration : BaseEntityConfiguration<CourseClass>
    {
        public override void Configure(EntityTypeBuilder<CourseClass> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            // Composite primary key
            entityTypeBuilder.HasKey(cc => new { cc.CourseId, cc.ClassId });

            // Relationship: CourseClass -> Course
            entityTypeBuilder.HasOne(cc => cc.Course)
                .WithMany(c => c.CourseClasses) // Add ICollection<CourseClass> in Course
                .HasForeignKey(cc => cc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: CourseClass -> Class
            entityTypeBuilder.HasOne(cc => cc.Class)
                .WithMany(c => c.CourseClasses) // Add ICollection<CourseClass> in Class
                .HasForeignKey(cc => cc.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
