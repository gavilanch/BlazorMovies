using AutoMapper;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend.Helpers;
using BlazorMovies.SharedBackend.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovies.SharedBackend
{
    public static class IServiceCollectionBlazorMovies
    {
        public static IServiceCollection AddBlazorMovies(this IServiceCollection services)
        {
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IGenreRepository, GenresRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddAutoMapper(new[] { typeof(AutomapperProfiles).Assembly });
            services.AddScoped<IFileStorageService, AzureStorageService>();

            return services;
        }
    }
}
