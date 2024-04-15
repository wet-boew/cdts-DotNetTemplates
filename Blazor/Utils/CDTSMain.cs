using Microsoft.JSInterop;

namespace GoC.WebTemplate.Blazor.Utils
{
    public static class CDTSMain
    {
        public static async Task InstallCDTS(IJSRuntime js, string lang)
        {
            await js.InvokeVoidAsync("cdtsBlazor.installCDTS", lang);
        }
    }
}
