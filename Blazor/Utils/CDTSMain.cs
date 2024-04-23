using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using Microsoft.JSInterop;

namespace GoC.WebTemplate.Blazor.Utils
{
    public static class CDTSMain
    {
        public static async Task InstallCDTS(IJSRuntime js, string lang, SetupBase setupBase = null, bool isApp = false)
        {
            await js.InvokeVoidAsync("cdtsBlazor.installCDTS", lang);

            setupBase ??= new SetupBase();

            await js.InvokeVoidAsync("cdtsBlazor.setRefTop", JsonPlainSerializationHelper.SerializeToJson(setupBase), isApp);
        }
    }
}
