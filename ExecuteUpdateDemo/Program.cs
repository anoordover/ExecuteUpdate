// See https://aka.ms/new-console-template for more information

using ExecuteUpdateDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var sp = new ServiceCollection()
    .AddDbContext<DemoDbContext>(options =>
    {
        options.UseOracle(
                "Data Source=dev;Persist Security Info=True;User ID=demo;Password=demo;");
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
    catch (Exception exception)
    {
        Console.WriteLine(exception);
    }

    Console.WriteLine("===========================================");
    Console.WriteLine("===========================================");
    Console.WriteLine("===========================================");
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
    catch (Exception exception)
    {
        Console.WriteLine(exception);
    }
}
