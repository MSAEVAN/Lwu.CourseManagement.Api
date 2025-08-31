using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Application.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? ModifiedByUserId { get; set; }
        public Guid? DeletedByUserId { get; set; }


        [ForeignKey(nameof(CreatedByUserId))]
        public AppUser CreatedByUser { get; set; }
        [ForeignKey(nameof(ModifiedByUserId))]
        public AppUser ModifiedByUser { get; set; }
        [ForeignKey(nameof(DeletedByUserId))]
        public AppUser DeletedByUser { get; set; }
    }
}
