using EmployeeCrudOperation_27_07.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudOperation_27_07
{
    public class EmployeeDbContext:DbContext
    {
        public DbSet<Employee>? Employee { set; get; }
        public EmployeeDbContext() { }
        public EmployeeDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder option)
        //{
        //    option.UseSqlServer(@"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=EmployeeCrud;Integrated Security=True");

        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasIndex(Emp => Emp.Email).IsUnique();


        }

    }
}
