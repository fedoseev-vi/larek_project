using larek_order.Models;
using Microsoft.EntityFrameworkCore;

namespace larek_order.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(u => new {u.order_id});
            modelBuilder.Entity<Status>().HasKey(u => u.status_id);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Status).WithMany(b => b.Order)
                .HasForeignKey(b => b.status_id);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Status>().ToTable("Status");
        }

    }
}