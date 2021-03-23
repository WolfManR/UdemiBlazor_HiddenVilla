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
        
        public static async ValueTask SwalSuccess(this IJSRuntime self, string message, string title)
        {
            await self.InvokeVoidAsync("ShowSwal", "success", title, message);
        }
        
        public static async ValueTask SwalError(this IJSRuntime self, string message, string title)
        {
            await self.InvokeVoidAsync("ShowSwal", "error", title, message);
        }
    }
}