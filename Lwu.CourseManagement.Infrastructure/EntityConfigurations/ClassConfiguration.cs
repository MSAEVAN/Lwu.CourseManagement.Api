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
    public class ClassConfiguration : BaseEntityConfiguration<Class>
    {
        public override void Configure(EntityTypeBuilder<Class> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(t => t.Name)
                .HasMaxLength(128)
                .IsRequired();

            entityTypeBuilder.Property(t => t.Room)
                .HasMaxLength(255);

            // Relationships
            entityTypeBuilder.HasMany(c => c.CourseClasses)
                .WithOne(cc => cc.Class)
                .HasForeignKey(cc => cc.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.HasMany(c => c.Enrollments)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
