using Microsoft.EntityFrameworkCore;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore_Infrastructure.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

    }
}
