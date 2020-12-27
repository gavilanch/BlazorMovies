using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Components.Helpers
{
    public interface IDisplayMessage
    {
        ValueTask DisplayErrorMessage(string message);
        ValueTask DisplaySuccessMessage(string message);
    }
}
