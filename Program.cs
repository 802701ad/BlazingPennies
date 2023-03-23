using BlazingPennies;
using BlazingPennies.Shared;
using BlazingPennies.Shared.BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*AUTH*/
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
/*END AUTH*/

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
