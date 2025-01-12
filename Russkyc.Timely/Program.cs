using BlazorComponentBus;
using Blazored.LocalStorage;
using Magic.IndexedDb;
using Magic.IndexedDb.Extensions;
using Magic.IndexedDb.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Russkyc.Timely;
using Russkyc.Timely.Models.Constants;
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
await app.RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
    
    services.AddSingleton<ComponentBus>();
    services.AddPWAUpdater();
    services.AddMudServices();
    services.AddBlazoredLocalStorageAsSingleton();
    
    var localEncryptionKey = "zQfTuWnZi8u7x!A%C*F-JaBdRlUkXp2l";

    services.AddBlazorDB(options =>
    {
        options.Name = StringValues.IndexedDbStoreId;
        options.Version = "1";
        options.EncryptionKey = localEncryptionKey;
        options.StoreSchemas = SchemaHelper.GetAllSchemas(StringValues.IndexedDbStoreId); // builds entire database schema for you based on attributes
    });
}