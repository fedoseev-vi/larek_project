using larek_catalogue.Models;
using Microsoft.EntityFrameworkCore;

namespace larek_catalogue.Data
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext(DbContextOptions<CatalogueContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasKey(u => u.brand_id);
            modelBuilder.Entity<Category>().HasKey(u => u.category_id);
            modelBuilder.Entity<Product>().HasKey(u => u.product_id);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Category).WithMany(b => b.Product)
                .HasForeignKey(b => b.category_id);
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Brand).WithMany(b => b.Product)
                .HasForeignKey(b => b.brand_id);

            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
        }

    }
}