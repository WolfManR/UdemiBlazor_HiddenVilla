using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenVilla_Server.Helper
{
    public static class IJSRuntimeExtensions
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime self, string message)
        {
            await self.InvokeVoidAsync("ShowToastr", "success", message);
        }
        
        public static async ValueTask ToastrError(this IJSRuntime self, string message)
        {
            await self.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}