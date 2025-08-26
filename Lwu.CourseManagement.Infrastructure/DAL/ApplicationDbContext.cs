using Lwu.CourseManagement.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.DAL
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Student).WithOne(s => s.User).HasForeignKey<AppUser>(u => u.StudentId);
        }
    }
}
