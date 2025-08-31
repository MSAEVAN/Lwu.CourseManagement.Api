using Lwu.CourseManagement.Application.Entities;
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
            entityTypeBuilder.Property(t => t.Id).IsRequired();
            entityTypeBuilder.Property(t => t.CreatedByUserId).IsRequired();
            entityTypeBuilder.Property(t => t.CreatedOn).IsRequired();
        }
    }
}
