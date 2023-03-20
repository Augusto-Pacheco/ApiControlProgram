using ApiControlProgram.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;

namespace ApiControlProgram.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<Types> types { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<ApplicationRole> roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasKey(u => u.Id);

            modelBuilder.Entity<Companies>().HasKey(c => c.CompanyId);
            modelBuilder.Entity<Categories>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);
            modelBuilder.Entity<Tasks>().HasKey(t => t.TaskId);
            modelBuilder.Entity<Types>().HasKey(tp => tp.TypeId);

            modelBuilder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.ConcurrencyStamp)
                                               .Ignore(c => c.EmailConfirmed)
                                               .Ignore(c => c.LockoutEnabled)
                                               .Ignore(c => c.LockoutEnd)
                                               .Ignore(c => c.PhoneNumber)
                                               .Ignore(c => c.PhoneNumberConfirmed)
                                               .Ignore(c => c.SecurityStamp)
                                               .Ignore(c => c.TwoFactorEnabled);

            modelBuilder.Entity<IdentityRole>().Ignore(r => r.ConcurrencyStamp)
                                               .Ignore(r => r.NormalizedName);

            modelBuilder.Entity<Companies>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Companies)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Types)
                .WithOne(tp => tp.Tasks)
                .HasForeignKey<Types>(tp => tp.TypeId);

            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Categories)
                .WithOne(c => c.Tasks)
                .HasForeignKey<Categories>(c => c.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
