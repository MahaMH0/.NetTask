
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendPart.Models
{
    public class EcommerceWebSiteEntities :DbContext
    {
        public EcommerceWebSiteEntities(DbContextOptions<EcommerceWebSiteEntities> options):base(options)
        {
            
        }

        public DbSet<Product> Product { get; set; }
    }
}
