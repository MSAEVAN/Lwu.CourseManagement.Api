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
    public class StuffConfiguration : BaseEntityConfiguration<Staff>
    {
        public override void Configure(EntityTypeBuilder<Staff> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(200);

            // Relationship with AppUser (one-to-one)
            entityTypeBuilder.HasOne(s => s.User)
                .WithOne() // Assuming AppUser does not have a Staff navigation property
                .HasForeignKey<Staff>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust as needed

            // Optional: Index for UserId for faster lookups
            entityTypeBuilder.HasIndex(s => s.UserId)
                .IsUnique(); // Only if each AppUser can have one Staff record
        }
    }
}
