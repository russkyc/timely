using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Russkyc.Timely;
using Russkyc.Timely.Services.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddBesqlDbContextFactory<AppDbContext>(options => options.UseSqlite("Data Source=timely.sqlite3"));

var app = builder.Build();

// To Create database and apply migrations
await using (var scope = app.Services.CreateAsyncScope())
{
    await using var dbContext = await scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<AppDbContext>>()
        .CreateDbContextAsync();

#if DEBUG
    dbContext.Database.EnsureDeleted();
#endif

    await dbContext.Database.EnsureCreatedAsync();
}

await app.RunAsync();