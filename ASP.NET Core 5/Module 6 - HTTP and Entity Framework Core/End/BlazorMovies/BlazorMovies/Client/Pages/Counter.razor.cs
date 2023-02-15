using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace BlazorMovies.Client.Pages
{
    public partial class Counter
    {
        [Inject] IJSRuntime js { get; set; }

        private int currentCount = 0;
        private static int currentCountStatic = 0;
        IJSObjectReference module;

        [JSInvokable]
        public async Task IncrementCount()
        {

            var array = new double[] { 1, 2, 3, 4, 5 };
            var max = array.Maximum();
            var min = array.Minimum();

            module = await js.InvokeAsync<IJSObjectReference>("import", "./js/Counter.js");
            await module.InvokeVoidAsync("displayAlert", $"Max is {max} and min is {min}");

            currentCount++;
            currentCountStatic++;
            await js.InvokeVoidAsync("dotnetStaticInvocation");
        }

        private async Task IncrementCountJavaScript()
        {
            await js.InvokeVoidAsync("dotnetInstanceInvocation",
                DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> GetCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
