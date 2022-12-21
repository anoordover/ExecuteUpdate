using Microsoft.EntityFrameworkCore;

namespace ExecuteUpdateDemo.Data;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.Entity<Credit>()
            .OwnsOne<Period>(c => c.Period);
    */
    }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Declaration> Declarations { get; set; }
}