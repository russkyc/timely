using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Russkyc.Timely;
using Russkyc.Timely.Services.Data;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// 👇 Add the condition that determine root components are 
//    already registered via prerednered HTML contents.
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<TimelyWebApp>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

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

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });

    services.AddPWAUpdater();
    services.AddMudServices();
    services.AddBlazoredLocalStorageAsSingleton();
    services.AddBesqlDbContextFactory<AppDbContext>(options => options.UseSqlite("Data Source=timely.sqlite3"));

}