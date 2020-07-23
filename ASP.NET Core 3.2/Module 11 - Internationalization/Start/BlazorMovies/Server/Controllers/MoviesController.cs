using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend;
using BlazorMovies.SharedBackend.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesController(
            IMoviesRepository moviesRepository
            )
        {
            this.moviesRepository = moviesRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            return await moviesRepository.GetIndexPageDTO();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DetailsMovieDTO>> Get(int id)
        {
            var model = await moviesRepository.GetDetailsMovieDTO(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Movie>>> Filter(FilterMoviesDTO filterMoviesDTO)
        {
            var paginatedResponse = await moviesRepository.GetMoviesFiltered(filterMoviesDTO);
            HttpContext.InsertPaginationParametersInResponse(paginatedResponse.TotalAmountPages);
            return paginatedResponse.Response;
        }

        [HttpGet("update/{id}")]
        public async Task<ActionResult<MovieUpdateDTO>> PutGet(int id)
        {
            var model = await moviesRepository.GetMovieForUpdate(id);
            if (model == null) { return NotFound(); }
            return model;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Movie movie)
        {
            return await moviesRepository.CreateMovie(movie);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Movie movie)
        {
            var movieDB = await moviesRepository.GetDetailsMovieDTO(movie.Id);

            if (movieDB == null) { return NotFound(); }

            await moviesRepository.UpdateMovie(movie);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await moviesRepository.GetDetailsMovieDTO(id);
            if (movie == null)
            {
                return NotFound();
            }

            await moviesRepository.DeleteMovie(id);
            return NoContent();
        }
    }
}
