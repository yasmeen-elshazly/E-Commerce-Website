using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Context.Context
{
    public class EcommerceDbContext : IdentityDbContext<User>
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(product =>
            {
                
                product.HasKey(p => p.ProductID);
                product.Property(p => p.Name).IsRequired();
                product.Property(p => p.Status).HasDefaultValue("Created");
                product.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Category>(category =>
            {
                category.HasKey(p => p.CategoryID);
                category.Property(p => p.Name).IsRequired();
            });

            builder.Entity<Order>(order =>
            {
                order.HasKey(o => o.OrderID);
                order.Property(o => o.Status).IsRequired().HasDefaultValue("Success");
                order.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OrderProduct>(orderproduct =>
            {
                orderproduct.HasKey(op => new
                {
                    op.OrderID,
                    op.ProductID,
                });
                orderproduct.HasOne(op => op.Product)
                    .WithMany(p => p.Products)
                    .HasForeignKey(op => op.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);

                orderproduct.HasOne(op => op.Order)
                    .WithMany(p => p.Products)
                    .HasForeignKey(op => op.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
