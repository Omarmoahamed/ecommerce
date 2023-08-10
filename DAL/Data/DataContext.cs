
using Ecommerce.DAL.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.models;

namespace Ecommerce.DAL.Data
{
    public class DataContext:IdentityDbContext<User>
{
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) 
        {

        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<discount> Discounts => Set<discount>();

        public DbSet<discountapplied_product> discountapplied_Products=> Set<discountapplied_product>();

        public DbSet<Rate> rates => Set<Rate>();

        public DbSet<Shopping_cart_item> shopping_cart_items => Set<Shopping_cart_item>();

        public DbSet<Order> orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<User>().Property(u => u.specs).HasConversion<int>();

            modelBuilder.Entity<discount>().Property(d => d.discount_type).HasConversion<int>();



        }

    }
}
