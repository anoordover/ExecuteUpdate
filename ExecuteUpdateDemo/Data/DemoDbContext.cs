using Microsoft.EntityFrameworkCore;

namespace ExecuteUpdateDemo.Data;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Declaration> Declarations { get; set; }
}