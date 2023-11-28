using Blazor;
using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var startup = new Startup(builder.Configuration); // My custom startup class.

startup.ConfigureServices(builder.Services); // Add services to the container.

builder.Services.AddLocalization();

//builder.Services.ConfigureGoCTemplateRequestLocalization();

//await builder.Build().RunAsync();

builder.Services.AddHttpContextAccessor();

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("en-CA");
    await js.InvokeVoidAsync("blazorCulture.set", "en-CA");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await js.InvokeVoidAsync("installCDTS", CultureInfo.CurrentCulture.Parent.IetfLanguageTag);
//await js.InvokeVoidAsync("installCDTS", CultureInfo.CurrentCulture.Parent.IetfLanguageTag);

await host.RunAsync();






