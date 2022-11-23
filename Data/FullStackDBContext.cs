using FullStack.Api.Modals;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employees> EmployeeTable { get; set; }

    }
}
