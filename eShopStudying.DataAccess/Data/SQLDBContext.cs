using eShopStudying.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShopStudying.DataAccess;

public class SQLDBContext : IdentityDbContext
{
    public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
}