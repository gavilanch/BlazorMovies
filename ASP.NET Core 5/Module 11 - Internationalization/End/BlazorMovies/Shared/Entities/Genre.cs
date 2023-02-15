using BlazorMovies.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(
             ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = nameof(Resource.required)
            )]
        public string Name { get; set; }
        public List<MoviesGenres> MoviesGenres { get; set; } = new List<MoviesGenres>();
    }
}
