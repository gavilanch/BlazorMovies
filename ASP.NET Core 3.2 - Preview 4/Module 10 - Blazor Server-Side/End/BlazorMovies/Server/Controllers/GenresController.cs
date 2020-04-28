using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository genresRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            this.genresRepository = genreRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await genresRepository.GetGenres();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await genresRepository.GetGenre(id);
            if (genre == null) { return NotFound(); }
            return genre;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Genre genre)
        {
            await genresRepository.CreateGenre(genre);
            return genre.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genre genre)
        {
            await genresRepository.UpdateGenre(genre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await genresRepository.GetGenre(id);
            if (genre == null)
            {
                return NotFound();
            }

            await genresRepository.DeleteGenre(id);
            return NoContent();
        }
    }
}
