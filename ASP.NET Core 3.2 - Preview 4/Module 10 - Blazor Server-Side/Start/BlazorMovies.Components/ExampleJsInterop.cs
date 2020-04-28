using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorMovies.Components
{
    public static class ExampleJsInterop
    {
        public static ValueTask<string> Prompt(this IJSRuntime jsRuntime, string message)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }
    }
}
