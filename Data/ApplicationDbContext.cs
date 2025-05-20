using EmployeeCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCompany.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define one-to-many relationship: Company -> Employees
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete employees when company is deleted

            base.OnModelCreating(modelBuilder);
        }
    }

  


}
