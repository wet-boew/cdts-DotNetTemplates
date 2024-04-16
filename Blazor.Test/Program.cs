using Blazor.Test;
using GoC.WebTemplate.Blazor.Utils;
using GoC.WebTemplate.Components.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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

//OPTIONAL: Custom SetupBase object
SetupBase setupBase = new SetupBase();
setupBase.ExitSecureSite = new ExitSecureSite()
{
    ExitScript = true,
    DisplayModal = true
};

await CDTSMain.InstallCDTS(js, CultureInfo.CurrentCulture.Parent.IetfLanguageTag, setupBase);

await host.RunAsync();
