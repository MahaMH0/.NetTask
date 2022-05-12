using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebSiteAPIs.Models
{
    public class EcommerceWebSiteEntities : IdentityDbContext<ApplicationUser> //DbContext
    {
        public EcommerceWebSiteEntities()
        {

        }
        public EcommerceWebSiteEntities(DbContextOptions options) : base(options)
        {


        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ShopingCartItem> ShoppingCartItems { get; set;}
    }
}
