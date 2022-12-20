using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExecuteUpdateDemo.Data;

public class DemoContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
        optionsBuilder.UseSqlServer("Server=172.17.0.1,1433;Database=myDataBase;User Id=sa;Password=StrongPassword;Encrypt=false;");

        return new DemoDbContext(optionsBuilder.Options);
    }
}
