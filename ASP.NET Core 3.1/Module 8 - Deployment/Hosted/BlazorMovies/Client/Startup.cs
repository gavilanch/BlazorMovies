using Blazor.FileReader;
using BlazorMovies.Client.Auth;
using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorMovies.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, RepositoryInMemory>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IDisplayMessage, DisplayMessage>();
            services.AddScoped<IUsersRepository, UserRepository>();

            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddAuthorizationCore();

            services.AddScoped<JWTAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
            );
            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
               provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
                );

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
