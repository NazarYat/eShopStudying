using eShopStudying.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopStudying.DataAccess;

public class SQLDBContext : DbContext
{
    public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
}