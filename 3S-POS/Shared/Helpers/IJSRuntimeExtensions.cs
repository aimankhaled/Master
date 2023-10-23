using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Helpers
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask DisplayMessage(this IJSRuntime js, string title, string message,
       SweetAlertMessageType sweetAlertMessageType)
        {
            return js.InvokeVoidAsync("Swal.fire", title, message, sweetAlertMessageType.ToString());
        }
        public enum SweetAlertMessageType
        {
            question,
            warning,
            error,
            success,
            info
        }

    }
}
