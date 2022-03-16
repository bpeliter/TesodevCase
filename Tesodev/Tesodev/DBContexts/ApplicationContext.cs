using Tesodev.Models;
using Microsoft.EntityFrameworkCore;

namespace Tesodev.DBContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source = DESKTOP-EK22GQD\SQLEXPRESS; Initial Catalog = ApplicationDB; Persist Security Info=True;Integrated Security=SSPI;");
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary Keys
            modelBuilder.Entity<Customer>().HasKey(s => s.CustomerId);
            modelBuilder.Entity<Order>().HasKey(s => s.OrderId);
            modelBuilder.Entity<Product>().HasKey(s => s.ProductId);
            modelBuilder.Entity<Address>().HasKey(s => s.CityCode);

            //Customer Properties
            modelBuilder.Entity<Customer>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(s => s.Email).IsRequired();
            modelBuilder.Entity<Customer>().Property(s => s.UpdatedAt).IsRequired();
            modelBuilder.Entity<Customer>().Property(s => s.CreatedAt).IsRequired();

            modelBuilder.Entity<Customer>().HasOne(s => s.Address).WithMany().IsRequired();

            //Order Properties
            modelBuilder.Entity<Order>().Property(s => s.Quantity).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Price).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Status).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.Quantity).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.CreatedAt).IsRequired();
            modelBuilder.Entity<Order>().Property(s => s.UpdatedAt).IsRequired();

            modelBuilder.Entity<Order>().HasOne(s => s.Address).WithMany().IsRequired();
            modelBuilder.Entity<Order>().HasOne(s => s.Product).WithMany().IsRequired();

            //Address Properties
            modelBuilder.Entity<Address>().Property(s => s.AddressLine).IsRequired();
            modelBuilder.Entity<Address>().Property(s => s.City).IsRequired();
            modelBuilder.Entity<Address>().Property(s => s.CityCode).IsRequired();
            modelBuilder.Entity<Address>().Property(s => s.Country).IsRequired();
            //Product Properties
            modelBuilder.Entity<Product>().Property(s => s.ImageUrl).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.Name).IsRequired();

            //Connections


            base.OnModelCreating(modelBuilder);
        }
    }
}
