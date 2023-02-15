using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Repositories
{
    public interface IRatingRepository
    {
        Task Vote(MovieRating movieRating);
    }
}
