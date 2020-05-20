using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorMovies.Server
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>().HasKey(x => new { x.MovieId, x.PersonId });
            modelBuilder.Entity<MoviesGenres>().HasKey(x => new { x.MovieId, x.GenreId });

            //var userAdminId = "df4d3070-5e26-4f78-bdd6-f9bb12f55a40";

            //var hasher = new PasswordHasher<IdentityUser>();

            //var userAdmin = new IdentityUser()
            //{
            //    Id = userAdminId,
            //    Email = "felipe@hotmail.com",
            //    UserName = "felipe@hotmail.com",
            //    NormalizedEmail = "felipe@hotmail.com",
            //    NormalizedUserName = "felipe@hotmail.com",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "aA123456!")
            //};

            //modelBuilder.Entity<IdentityUser>().HasData(userAdmin);

            //modelBuilder.Entity<IdentityUserClaim<string>>()
            //    .HasData(new IdentityUserClaim<string>() { 
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        ClaimValue = "Admin",
            //        UserId = userAdminId
            //    }); 

            modelBuilder.Entity<Genre>().HasData(new Genre() { Id = 2, Name = "Comedy" });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}
