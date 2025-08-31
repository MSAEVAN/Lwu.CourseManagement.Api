using Lwu.CourseManagement.Application.Common.Interfaces;
using Lwu.CourseManagement.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lwu.CourseManagement.Infrastructure.DAL
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggedInUserService _loggedInUserService;

        public ApplicationDbContext(DbContextOptions options, ILoggedInUserService currentUserService, IConfiguration configuration) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _configuration = configuration;
            _loggedInUserService = currentUserService;
        }


        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Staff> Staffs => Set<Staff>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            // Application default seed data
            modelBuilder.Entity<AppUser>().HasData(
               new AppUser()
               {
                   Id = Guid.Parse("EAA6081C-16F1-41BE-9153-5662BC03E9FC"),
                   Username = "LwuAdmin",
                   Email = "admin@mail.com",
                   UserPrincipal = "admin",
                   FullName = "MSA Evan",
                   Role = "Stuff",
                   PasswordHash = "$2a$11$9.MV93UKWiuZtTmqzHmb2.ENRvyEHTB726NQgquEC77X4FGWTVuIq", // admin123
                   CreatedByUserId = Guid.Parse("EAA6081C-16F1-41BE-9153-5662BC03E9FC"),
                   CreatedOn = new DateTime(2025, 08, 31, 0, 0, 0, DateTimeKind.Utc),
                   IsDeleted = false
               }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedByUserId = _loggedInUserService.Id;
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.ModifiedByUserId = entry.Entity.CreatedByUserId;
                        entry.Entity.ModifiedOn = entry.Entity.CreatedOn;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedByUserId = _loggedInUserService.Id;
                        entry.Entity.ModifiedOn = DateTime.Now;
                        break;
                }
            }
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}
