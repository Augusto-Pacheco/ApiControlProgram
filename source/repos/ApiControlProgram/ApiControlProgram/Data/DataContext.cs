using ApiControlProgram.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>().HasKey(c => c.CompanyId);
            modelBuilder.Entity<Categories>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);
            modelBuilder.Entity<Tasks>().HasKey(t => t.TaskId);
            modelBuilder.Entity<Types>().HasKey(tp => tp.TypeId);

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
