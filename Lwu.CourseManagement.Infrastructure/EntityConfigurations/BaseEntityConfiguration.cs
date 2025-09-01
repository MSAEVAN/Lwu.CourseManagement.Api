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
    public class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>
        where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.Id);
            entityTypeBuilder.Property(t => t.Id).IsRequired();
            entityTypeBuilder.Property(t => t.CreatedByUserId).IsRequired();
            entityTypeBuilder.Property(t => t.CreatedOn).IsRequired();

            // Relationships (self-referencing to AppUser)
            entityTypeBuilder.HasOne(e => e.CreatedByUser)
                .WithMany() // many entities can be created by one user
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityTypeBuilder.HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entityTypeBuilder.HasOne(e => e.DeletedByUser)
                .WithMany()
                .HasForeignKey(e => e.DeletedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //
            entityTypeBuilder.Property(e => e.ModifiedOn).IsRequired(false);
            entityTypeBuilder.Property(e => e.DeletedOn).IsRequired(false);
        }
    }
}
