using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
using BlazorWasmUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
    .AddBulmaProviders()
    .AddFontAwesomeIcons();

await builder.Build().RunAsync();