using BlazorMovies.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public class ExampleImplementation : IExampleInterface
    {
        public string GetValue()
        {
            return "From WebAssembly";
        }
    }
}
