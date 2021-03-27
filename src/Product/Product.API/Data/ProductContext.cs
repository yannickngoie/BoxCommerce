using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API
{
    public class ProductContext : DbContext
    {
        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Attributes> Attributes { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Product>().ToTable("Product");
            modelBuilder.Entity<Attributes>().ToTable("Attribute");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
