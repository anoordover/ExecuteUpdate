// See https://aka.ms/new-console-template for more information

using ExecuteUpdateDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var sp = new ServiceCollection()
    .AddDbContext<DemoDbContext>(options =>
    {
        options.UseSqlServer(
                "Server=172.17.0.1,1433;Database=myDataBase;User Id=sa;Password=StrongPassword;Encrypt=false;");
        options.LogTo(Console.WriteLine);
    })
    .BuildServiceProvider();

using var db = sp.GetRequiredService<DemoDbContext>();
{
    var credits = db.Credits.ToList();

    Console.WriteLine(credits.Count);

    try
    {
        var s = db.Credits.Where(c => c.Id == 1)
            .Join(db.Declarations,
                c => c.ReferenceDeclaration,
                d => d.Reference,
                (credit, declaration) => new {credit, declaration})
            .ExecuteUpdate(calls => calls.SetProperty(
                c => c.credit.DeclarationId,
                c => c.declaration.Id));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    try
    {
        var r = db.Credits.Where(c => c.Id == 1)
            .Select(c => new
            {
                credit = c,
                declaration = db.Declarations
                    .First(d => d.Reference == c.ReferenceDeclaration)
            })
            .ExecuteUpdate(calls => calls.SetProperty(
                c => c.credit.DeclarationId,
                c => c.declaration.Id));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    try
    {
        var r = db.Credits.Where(c => c.Id == 1)
            .Select(c => new
            {
                credit = c,
                declaration = db.Declarations
                    .Where(d => d.Reference == c.ReferenceDeclaration)
                    .Select(d => d.Id)
                    .First()
            })
            .ExecuteUpdate(calls => calls.SetProperty(
                c => c.credit.DeclarationId,
                c => c.declaration));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}
