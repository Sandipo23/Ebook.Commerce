using EBook.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Ebook.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } // Add this line to include ApplicationUser in the DbContext,
                                                                     // this will allow you to perform CRUD operations on the ApplicationUser entity and
                                                                     // hold additional properties specific to your application's users
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder) // Override the OnModelCreating method to configure the entity relationships and keys
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey }); // Configure composite primary key for IdentityUserLogin
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(role => new { role.UserId, role.RoleId }); // Configure composite primary key for IdentityUserRole
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasKey(claim => claim.Id); // Configure primary key for IdentityUserClaim
            });
        }


    }
}
