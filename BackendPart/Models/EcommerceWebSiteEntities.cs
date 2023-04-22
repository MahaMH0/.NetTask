using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendPart.Models
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
    }
}
