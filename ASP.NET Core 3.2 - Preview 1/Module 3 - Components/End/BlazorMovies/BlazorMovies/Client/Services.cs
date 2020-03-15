using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client
{
    public class SingletonService
    {
        public int Value { get; set; }
    }

    public class TransientService
    {
        public int Value { get; set; }
    }
}
